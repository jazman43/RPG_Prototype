using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inputs;


namespace RPG.UI
{
    public class ShowHideIU : MonoBehaviour
    {
        [SerializeField] GameObject inventoryUI = null;

        private void Start()
        {
            inventoryUI.SetActive(false);
        }

        private void Update()
        {
            if (FindObjectOfType<InputActions>().ShowUI())
            {
                TurnOnOfUI();
            }
        }

        public void TurnOnOfUI()
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}

