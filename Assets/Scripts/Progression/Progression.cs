using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Progression
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Jareds/Stats/ New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses = null;


        Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookupTable = null;


        public float GetStat(Stats stat, CharacterClass characterClass, int level)
        {
            BuildLookUp();

            if (!lookupTable[characterClass].ContainsKey(stat))
            {
                return 0;
            }

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length == 0)
            {
                return 0;
            }

            if (levels.Length < level)
            {
                return levels[levels.Length - 1];
            }

            return levels[level - 1];
        }

        public int GetLevels(Stats stat, CharacterClass characterClass)
        {
            BuildLookUp();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookUp()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();

            foreach (ProgressionCharacterClass progressionCharacter in characterClasses)
            {
                var statLookupTable = new Dictionary<Stats, float[]>();

                foreach (PrgressionStat prgressionStat in progressionCharacter.stats)
                {
                    statLookupTable[prgressionStat.stat] = prgressionStat.levels;
                }


                lookupTable[progressionCharacter.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public PrgressionStat[] stats;
            //public float[] health;
        }


        [System.Serializable]
        class PrgressionStat
        {
            public Stats stat;
            public float[] levels;
        }

    }
}

