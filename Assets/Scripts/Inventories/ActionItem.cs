using System;
using UnityEngine;

namespace RPG.Inventories
{
   
    [CreateAssetMenu(menuName = ("Jareds/UI.InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
        
        [Tooltip("Does an instance of this item get consumed every time it's used.")]
        [SerializeField] bool consumable = false;

        
        public virtual bool Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
            return false;
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}