using RPG.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{

    [ExecuteAlways]
    public class SaveableEntiy : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        static Dictionary<string, SaveableEntiy> globalLookUp = new Dictionary<string, SaveableEntiy>();


        public string GetUniqueIdenerifer()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            Debug.Log("Capturing State For " + GetUniqueIdenerifer());
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
            
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();

                if (stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
            
            
        }


#if UNITY_EDITOR
        private void Update()
        {
            if(Application.IsPlaying(gameObject))
            {
                return;
            }
            if(string.IsNullOrEmpty(gameObject.scene.path)) { return; }

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property =serializedObject.FindProperty("uniqueIdentifier");

            if(string.IsNullOrEmpty(property.stringValue)|| !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookUp[property.stringValue] = this;
            
            Debug.Log("EDIT TIME");
        }

        private bool IsUnique(string stringValue)
        {
            if(!globalLookUp.ContainsKey(stringValue))
            {
                return true;
            }
            if (globalLookUp[stringValue] == this) { return true; }

            if (globalLookUp[stringValue] == null)
            {
                globalLookUp.Remove(stringValue);
                return true;
            }

            if (globalLookUp[stringValue].GetUniqueIdenerifer() != stringValue)
            {
                globalLookUp.Remove(stringValue);
                return true;
            }

            return false;
        }
#endif
    }
}

