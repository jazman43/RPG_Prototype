using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Inputs;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EquippablePickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Weapon pickUpEquippable = null;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] private float respawnTime = 5f;
        

        

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hit Pick Me UP");
            if (other.gameObject.tag == "Player")
            {
                //PickUp(other.gameObject);
            }


        }

        private void PickUp(GameObject subject)
        {
            if (healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }


            if (pickUpEquippable != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(pickUpEquippable);
            }

            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            HidePickUp();
            yield return new WaitForSeconds(seconds);
            ShowPickUp();
        }

        private void ShowPickUp()
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            foreach (Transform game in transform)
            {
                game.gameObject.SetActive(true);
            }
        }

        private void HidePickUp()
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            foreach (Transform game in transform)
            {
                game.gameObject.SetActive(false);
            }
        }

        public Cursors GetCursorType()
        {
            return Cursors.PickUp;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (GetComponent<InputActions>().InteractWithComponet())
            {
                PickUp(callingController.gameObject);
            }
            return true;
        }
    }
}

