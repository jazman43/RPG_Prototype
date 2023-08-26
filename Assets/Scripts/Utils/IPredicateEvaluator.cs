using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jareds.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}