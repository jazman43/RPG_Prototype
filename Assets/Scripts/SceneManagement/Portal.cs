using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Control;

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
        [SerializeField] private int sceneIndex = -1;
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


        public void OnUIClick()
        {
            Debug.Log("Start Game");
            StartCoroutine(Transition());
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
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            Debug.Log(playerController);
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.SaveGameState();

            yield return SceneManager.LoadSceneAsync(sceneIndex);

            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;

            Debug.Log("Travaling...");
            
            
            savingWrapper.LoadGameState();
            
            

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.SaveGameState();

            yield return new WaitForSeconds(fadeWaitTime);

            fader.FadeIn(fadeInTime);


            newPlayerController.enabled = true;
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

