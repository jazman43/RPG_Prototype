using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Progression
{
    public class Experice : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        
        public event Action onXpGained;

        /*private void Update()
        {
            
            
                GainExperience(Time.deltaTime * 1000);
        }*/

        public void GainExperience(float experienceGained)
        {
            experiencePoints += experienceGained;
            onXpGained();
        }

        public float GetExperience()
        {
            return experiencePoints;
        }
        
        public float GetMaxXP()
        {
            return GetComponent<BaseStats>().GetStat(Stats.ExperienceToLevelUp);
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }
    }
}

