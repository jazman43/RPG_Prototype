using UnityEngine;
using Jareds.Utils;

namespace RPG.Inventories
{
    
    [CreateAssetMenu(menuName = ("Jareds/UI.InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;
        [SerializeField] Condition equipCondition;

        

        public bool CanEquip(EquipLocation equipLocation, Equipment equipment)
        {
            if (equipLocation != allowedEquipLocation) return false;

            return equipCondition.Check(equipment.GetComponents<IPredicateEvaluator>());
        }

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}