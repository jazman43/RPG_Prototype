
using System.Collections.Generic;

using RPG.Progression;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("Jareds/Inventory/Equipable Item"))]
    public class StatsEquipableItem : EquipableItem, IModProvider
    {
        [SerializeField]
        Modifier[] additiveModifiers;
        [SerializeField]
        Modifier[] percentageModifiers;

        [System.Serializable]
        struct Modifier
        {
            public Stats stat;
            public float value;
        }   

       
        public IEnumerable<float> GetAdditiveMod(Stats stats)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat == stats)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageMod(Stats stats)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stats)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}
