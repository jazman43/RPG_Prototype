using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.UI.InteractableUI
{
    public class InteractableTextPopUp : MonoBehaviour
    {
        [SerializeField] GameObject interactableTextPrefab = null;

        public void SpawnInteractableText()
        {
            

            GameObject instance = Instantiate<GameObject>(interactableTextPrefab, transform);

            Destroy(instance, 0.1f);
        }
    }
}

