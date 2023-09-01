using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Progression;
using System;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "XP Effect", menuName = "Jareds/Abilities/Effects/XP", order = 0)]
    public class XPEffect : EffectStrategy
    {
        
        [SerializeField] float xPChange;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.GetTargets())
            {
                var xp = target.GetComponent<Experice>();
                if (xp)
                {
                    xp.GainExperience(xPChange);
                }
            }
            finished();
        }
    }
}
