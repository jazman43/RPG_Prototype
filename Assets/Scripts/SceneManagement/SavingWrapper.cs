using RPG.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "Save";
        [SerializeField] float fadeInTime = 0.2f;
        [SerializeField] float fadeOutTime = 0.2f;
        [SerializeField] int firstLevelBuildIndex = 1;
        [SerializeField] int menuLevelBuildIndex = 0;


        MyInputs inputActions;


        private void Awake()
        {
            
            inputActions = new MyInputs();
           
            //Debug.Log(inputActions);
        }

        public void ContinueGame()
        {
            if (!PlayerPrefs.HasKey(defaultSaveFile)) return;
            if (GetComponent<SavingSystem>().SaveExists(GetCurrentSave())) return;
            Debug.Log("Loading.. continue");
            StartCoroutine(LoadLastScene());
        }

        public void NewGame(string saveFile)
        {
            if (string.IsNullOrEmpty(saveFile)) return;
            SetCurrentSave(saveFile);
            StartCoroutine(LoadFirstScene());
        }

        public void LoadGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            ContinueGame();
        }

        public void LoadMenu()
        {
            StartCoroutine(LoadMenuScene());
        }



        private void SetCurrentSave(string saveFile)
        {
            Debug.Log(saveFile);
            PlayerPrefs.SetString(defaultSaveFile, saveFile);
        }

        private string GetCurrentSave()
        {
            return PlayerPrefs.GetString(defaultSaveFile);
        }


        private IEnumerator LoadLastScene()
        {   
            Fader fader = FindObjectOfType<Fader>();
            Debug.Log("loading last Scene");
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(GetCurrentSave());
            yield return fader.FadeIn(fadeInTime);
        }

        private IEnumerator LoadFirstScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        private IEnumerator LoadMenuScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
            yield return fader.FadeIn(fadeInTime);
        }

        

        public void SaveGameState()
        {
            GetComponent<SavingSystem>().Save(GetCurrentSave());
        }

        public void LoadGameState()
        {
            GetComponent<SavingSystem>().Load(GetCurrentSave());
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(GetCurrentSave());
        }

        public IEnumerable<string> ListSaves()
        {
            return GetComponent<SavingSystem>().ListSaves();
        }
    }
}

