using System.Collections;
using System.Collections.Generic;
using RPG.Progression;
using UnityEngine;

namespace RPG.Inventories
{
    public class StatsEquipment : Equipment, IModProvider
    {
        IEnumerable<float> IModProvider.GetAdditiveMod(Stats stats)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetAdditiveMod(stats))
                {
                    yield return modifier;
                }
            }
        }

        IEnumerable<float> IModProvider.GetPercentageMod(Stats stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetPercentageMod(stat))
                {
                    yield return modifier;
                }
            }
        }

    }
}
