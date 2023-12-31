using System;
using System.Collections.Generic;
using RPG.Saving;
using Jareds.Utils;
using UnityEngine;
using RPG.Quests;


namespace RPG.Progression
{
    public class TraitStore : MonoBehaviour, IModProvider, ISaveable, IPredicateEvaluator
    {
        [SerializeField] TraitBonus[] bonusConfig;
        [System.Serializable]
        class TraitBonus
        {
            public Trait trait;
            public Stats stat;
            public float additiveBonusPerPoint = 0;
            public float percentageBonusPerPoint = 0;
        }

        Dictionary<Trait, int> assignedPoints = new Dictionary<Trait, int>();
        Dictionary<Trait, int> stagedPoints = new Dictionary<Trait, int>();

        Dictionary<Stats, Dictionary<Trait, float>> additiveBonusCache;
        Dictionary<Stats, Dictionary<Trait, float>> percentageBonusCache;

        private void Awake()
        {
            additiveBonusCache = new Dictionary<Stats, Dictionary<Trait, float>>();
            percentageBonusCache = new Dictionary<Stats, Dictionary<Trait, float>>();
            foreach (var bonus in bonusConfig)
            {
                if (!additiveBonusCache.ContainsKey(bonus.stat))
                {
                    additiveBonusCache[bonus.stat] = new Dictionary<Trait, float>();
                }
                if (!percentageBonusCache.ContainsKey(bonus.stat))
                {
                    percentageBonusCache[bonus.stat] = new Dictionary<Trait, float>();
                }
                additiveBonusCache[bonus.stat][bonus.trait] = bonus.additiveBonusPerPoint;
                percentageBonusCache[bonus.stat][bonus.trait] = bonus.percentageBonusPerPoint;
            }
        }

        public int GetProposedPoints(Trait trait)
        {
            return GetPoints(trait) + GetStagedPoints(trait);
        }

        public int GetPoints(Trait trait)
        {
            return assignedPoints.ContainsKey(trait) ? assignedPoints[trait] : 0;
        }

        public int GetStagedPoints(Trait trait)
        {
            return stagedPoints.ContainsKey(trait)? stagedPoints[trait] : 0;
        }

        public void AssignPoints(Trait trait, int points)
        {
            if (!CanAssignPoints(trait, points)) return;

            stagedPoints[trait] = GetStagedPoints(trait) + points;
        }

        public bool CanAssignPoints(Trait trait, int points)
        {
            if (GetStagedPoints(trait) + points < 0) return false;
            if (GetUnassignedPoints() < points) return false;
            return true;
        }

        public int GetUnassignedPoints()
        {
            return GetAssignablePoints() - GetTotalProposedPoints();
        }

        public int GetTotalProposedPoints()
        {
            int total = 0;
            foreach (int points in assignedPoints.Values)
            {
                total += points;
            }
            foreach (int points in stagedPoints.Values)
            {
                total += points;
            }
            return total;
        }

        public void Commit()
        {
            foreach (Trait trait in stagedPoints.Keys)
            {
                assignedPoints[trait] = GetProposedPoints(trait);
            }
            stagedPoints.Clear();
        }

        public int GetAssignablePoints()
        {
            return (int)GetComponent<BaseStats>().GetStat(Stats.TotalTraitPoints);
        }

        public IEnumerable<float> GetAdditiveMod(Stats stat)
        {
            if (!additiveBonusCache.ContainsKey(stat)) yield break;

            foreach (Trait trait in additiveBonusCache[stat].Keys)
            {
                float bonus = additiveBonusCache[stat][trait];
                yield return bonus * GetPoints(trait);
            }
        }

        public IEnumerable<float> GetPercentageMod(Stats stat)
        {
            if (!percentageBonusCache.ContainsKey(stat)) yield break;

            foreach (Trait trait in percentageBonusCache[stat].Keys)
            {
                float bonus = percentageBonusCache[stat][trait];
                yield return bonus * GetPoints(trait);
            }
        }

        public object CaptureState()
        {
            return assignedPoints;
        }

        public void RestoreState(object state)
        {
            assignedPoints = new Dictionary<Trait, int>((IDictionary<Trait, int>)state);
        }

        public bool? Evaluate(QuestPredicateEnum questPredicate, string[] parameters)
        {
            if (questPredicate == QuestPredicateEnum.MinimumTrait)
            {
                if (Enum.TryParse<Trait>(parameters[0], out Trait trait))
                {
                    return GetPoints(trait) >= Int32.Parse(parameters[1]);
                } 
            }
            return null;
        }
    }
}