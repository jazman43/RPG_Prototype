using System.Collections;
using System.Collections.Generic;
using RPG.core.UI.Dragging;
using RPG.Inventories;
using RPG.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Inventories
{
    
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;
        [SerializeField] Image cooldownOverlay = null;

        
        ActionStore store;
        CooldownStore cooldownStore;

        
        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            store = player.GetComponent<ActionStore>();
            cooldownStore = player.GetComponent<CooldownStore>();
            store.storeUpdated += UpdateIcon;
            
        }

        private void Update() {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(GetItem());
            
        }

        

        public void AddItems(InventoryItem item, int number)
        {
            store.AddAction(item, index, number);
        }

        public InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int GetNumber()
        {
            return store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            store.RemoveItems(index, number);
        }

        

        private void UpdateIcon()
        {
            Debug.Log("Update Icon" + GetNumber());
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}
