using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {

        

        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                

                BinaryFormatter formatter = new BinaryFormatter();
                
                formatter.Serialize(stream, CaptureState());

                
            }

            
        }

        

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("loading from " + path);

            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                BinaryFormatter formatter= new BinaryFormatter();
                

                RestorState(formatter.Deserialize(stream));
                
                stream.Close();
            }
        }

       

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(SaveableEntiy saveable in FindObjectsOfType<SaveableEntiy>())
            {
                state[saveable.GetUniqueIdenerifer()] = saveable.CaptureState();
                
            }

            return null;
        }
        private void RestorState(object state)
        {
            Dictionary<string, object> stateLoadDictionary = (Dictionary<string, object>)state; 
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".bin");
        }
    }
}

