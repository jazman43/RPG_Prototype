using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Quests;

namespace Jareds.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(QuestPredicateEnum questPredicate, string[] parameters);
    }
}