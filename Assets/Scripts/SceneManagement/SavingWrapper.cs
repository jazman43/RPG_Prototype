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

        MyInputs inputActions;


        private void Awake()
        {
            
            inputActions = new MyInputs();
           
            //Debug.Log(inputActions);
        }

        private void Start()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
                Fader fader = FindObjectOfType<Fader>();
                fader.FadeOutImmediate();                
                yield return fader.FadeIn(3f);
            }
            
        }

        private void Update()
        {

            //Debug.Log(inputActions.PlayerActions.TEST_SAVE.IsPressed());

            if (inputActions.PlayerActions.TEST_SAVE.IsPressed())
            {
                Debug.Log("hello saving");
                
            }
            if(inputActions.PlayerActions.TEST_LOAD.IsPressed())
            {
                GetComponent<SavingSystem>().Load(defaultSaveFile);
            }
        }

        public void SaveGameState()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void LoadGameState()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}

