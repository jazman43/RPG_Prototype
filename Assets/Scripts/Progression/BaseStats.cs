using System.Collections;
using System;
using UnityEngine;
using Jareds.Utils;

namespace RPG.Progression
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,500)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;
        [SerializeField] private bool shouldUseMods = false;


        public event Action onLevelUp; 

        LazyJaredValue<int> currentLevel;
        Experice experice;

        private void Awake()
        {
            experice = GetComponent<Experice>();
            currentLevel = new LazyJaredValue<int>(CalculateLevel);
        }

        private void Start()
        {
            
            currentLevel.ForceInit();
        }

        private void OnEnable()
        {            
            if (experice != null)
            {
                experice.onXpGained += UpdateLevel;
            }            
        }

        private void OnDisable()
        {
            if (experice != null)
            {
                experice.onXpGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                Debug.Log("Level UP!");
                currentLevel.value = newLevel;
                onLevelUp();
            }
        }

        public float GetStat(Stats stats)
        {
            return GetBaseStat(stats) + GetAdditiveMod(stats) * (1 + GetPercentageMod(stats) / 100);
        }

        

        private float GetBaseStat(Stats stats)
        {
            
            return progression.GetStat(stats, characterClass, GetLevel());
        }


        public int GetLevel()
        {            
            return currentLevel.value;
        }

        private int CalculateLevel()
        {
            
            if (experice == null) return startingLevel;

            float currentXp = experice.GetExperience();

            int maxLevel = progression.GetLevels(Stats.ExperienceToLevelUp, characterClass);

            for(int levels = 1; levels <= maxLevel; levels++)
            {
                float xPToLevelUp = progression.GetStat(Stats.ExperienceToLevelUp, characterClass, levels);

                if(xPToLevelUp > currentXp)
                {
                    return levels;
                }
            }

            return maxLevel +1;
        }

        private float GetAdditiveMod(Stats stats)
        {
            if (!shouldUseMods) return 0;

            float total = 0f;

            foreach(IModProvider provider in GetComponents<IModProvider>())
            {
                foreach(float mod in provider.GetAdditiveMod(stats))
                {
                    total += mod;
                }
            }

            return total;
        }

        private float GetPercentageMod(Stats stats)
        {
            if (!shouldUseMods) return 0;

            float total = 0f;

            foreach (IModProvider provider in GetComponents<IModProvider>())
            {
                foreach (float mod in provider.GetPercentageMod(stats))
                {
                    total += mod;
                }
            }

            return total;
        }
    }
}
