using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Combat
{
    public class EquippablePickup : MonoBehaviour
    {
        [SerializeField] private Weapon pickUpEquippable = null;
        

        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hit Pick Me UP");
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(pickUpEquippable);

               

                Destroy(gameObject);
            }
        }
    }
}

