﻿using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using UnityEngine;
using RPG.Inputs;


namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;
        InputActions input;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
            input = FindObjectOfType<InputActions>();
        }

        /*
        public Cursors GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return Cursors.PickUp;
            }
            else
            {
                return Cursors.FullPickup;
            }
        }
        */

        public bool HandleRaycast(PlayerController callingController)
        {
            if (input.InteractWithComponet())
            {
                pickup.PickupItem();
            }
            return true;
        }
    }
}
