using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Saving
{
    public class SaveableEntiy : MonoBehaviour
    {
        public string GetUniqueIdenerifer()
        {
            return "0";
        }

        public object CaptureState()
        {
            Debug.Log("Capturing State For " + GetUniqueIdenerifer());
            return null;
        }

        public void RestoreState(object state)
        {
            Debug.Log("Restoreing State For " + GetUniqueIdenerifer());
        }
    }
}

