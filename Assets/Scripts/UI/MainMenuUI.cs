using System;
using Jareds.Utils;
using RPG.SceneManagement;
using UnityEngine;
using TMPro;
using RPG.Dialogue;


namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        LazyJaredValue<SavingWrapper> savingWrapper;

        [SerializeField] TMP_InputField newGameNameField;

        private void Awake() {
            savingWrapper = new LazyJaredValue<SavingWrapper>(GetSavingWrapper);
        }

        private void Start()
        {
            PlayerConversant playerConversant = FindObjectOfType<PlayerConversant>();
            playerConversant.SetPlayerName(newGameNameField.text);
        }
        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();
        }

        public void ContinueGame()
        {
            savingWrapper.value.ContinueGame();
        }

        public void NewGame()
        {
            Debug.Log(newGameNameField.text);
            savingWrapper.value.NewGame(newGameNameField.text);
        }

        

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}