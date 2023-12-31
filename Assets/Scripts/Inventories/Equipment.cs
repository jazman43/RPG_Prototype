﻿using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using Jareds.Utils;
using RPG.Quests;


namespace RPG.Inventories
{
    
    public class Equipment : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        
        Dictionary<EquipLocation, EquipableItem> equippedItems = new Dictionary<EquipLocation, EquipableItem>();

        
        public event Action equipmentUpdated;

        
        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if (!equippedItems.ContainsKey(equipLocation))
            {
                return null;
            }

            return equippedItems[equipLocation];
        }

        
        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            Debug.Assert(item.CanEquip(slot, this));

            equippedItems[slot] = item;

            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        
        public void RemoveItem(EquipLocation slot)
        {
            equippedItems.Remove(slot);
            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        
        public IEnumerable<EquipLocation> GetAllPopulatedSlots()
        {
            return equippedItems.Keys;
        }

        

        object ISaveable.CaptureState()
        {
            var equippedItemsForSerialization = new Dictionary<EquipLocation, string>();
            foreach (var pair in equippedItems)
            {
                equippedItemsForSerialization[pair.Key] = pair.Value.GetItemID();
            }
            return equippedItemsForSerialization;
        }

        void ISaveable.RestoreState(object state)
        {
            equippedItems = new Dictionary<EquipLocation, EquipableItem>();

            var equippedItemsForSerialization = (Dictionary<EquipLocation, string>)state;

            foreach (var pair in equippedItemsForSerialization)
            {
                var item = (EquipableItem)InventoryItem.GetFromID(pair.Value);
                if (item != null)
                {
                    equippedItems[pair.Key] = item;
                }
            }

            equipmentUpdated?.Invoke();
        }

        public bool? Evaluate(QuestPredicateEnum questPredicate, string[] parameters)
        {
            //HasItemEquiped need to be a enum will change one day
            if (questPredicate == QuestPredicateEnum.HasItemEquiped)
            {
                foreach (var item in equippedItems.Values)
                {
                    if (item.GetItemID() == parameters[0])
                    {
                        return true;
                    }
                }
                return false;
            }
            return null;
        }
    }
}