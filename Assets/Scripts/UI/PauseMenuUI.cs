using RPG.Control;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        PlayerController playerController;

        private void Awake() {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            Debug.Log("PauseGame!");
            
            if (playerController == null) return;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            playerController.enabled = false;
        }

        private void OnDisable()
        {
            Debug.Log("UNPauseGame!");
            
            if (playerController == null) return;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            playerController.enabled = true;
        }

        public void Save()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.SaveGameState();
        }

        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.SaveGameState();
            savingWrapper.LoadMenu();
        }
    }
}