using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;



namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E, F
        }


        [SerializeField] private float fadeInTime = 3f;
        [SerializeField] private float fadeOutTime = 3f;
        [SerializeField] private float fadeWaitTime = 1f;
        [SerializeField]private int sceneIndex = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
            Debug.Log("Traving..");
        }

        private IEnumerator Transition()
        {
            if(sceneIndex < 0)
            {
                Debug.LogError("Scene Not Set!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            Debug.Log("Traving...");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            //player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this)
                {
                    continue;
                }
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
    }


}

