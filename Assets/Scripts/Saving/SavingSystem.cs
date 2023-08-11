using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);

            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                sceneIndex = (int)state["lastSceneBuildIndex"];
                
            }
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            RestorState(state);
        }
        

        public void Save(string saveFile)
        {           
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        

        public void Load(string saveFile)
        {
           
            RestorState(LoadFile(saveFile));
        }

        

        private void SaveFile(string saveFile, Dictionary<string, object> state)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("loading from " + path);

            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }


            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        public void Delete(string defaultSaveFile)
        {
            File.Delete(GetPathFromSaveFile(defaultSaveFile));
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            
            foreach(SaveableEntiy saveable in FindObjectsOfType<SaveableEntiy>())
            {
                state[saveable.GetUniqueIdenerifer()] = saveable.CaptureState();
                
            }
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;

        }
        private void RestorState(Dictionary<string, object> state)
        {            

            foreach(SaveableEntiy saveable in FindObjectsOfType<SaveableEntiy>())
            {
                string id = saveable.GetUniqueIdenerifer();

                if(state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
                
            }

        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".SaveRPG");
        }
    }
}

