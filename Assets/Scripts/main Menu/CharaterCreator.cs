using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;

namespace RPG.Menus
{
    public class CharaterCreator : MonoBehaviour,ISaveable
    {
        [SerializeField] private List<GameObject> playerPreFab = null;
        [SerializeField] private List<Material> characterMaterials = null;
        //[SerializeField] private Renderer characterRenderer = null;

        //[SerializeField] private GameObject charaterCreatorUI = null;


        private int characterPosition = 0;
        private int characterCount = 0;

        //private int materialPosition = 0;

        private void Start()
        {         
            if(SceneManager.GetActiveScene().buildIndex > 0)
            {
                for (int i = 0; i < playerPreFab.Count; i++)
                {
                    playerPreFab[i].SetActive(false);   
                }
                playerPreFab[characterPosition].gameObject.SetActive(true);
                
                
                return; 
            }

            for (int i = 0; i < playerPreFab.Count; i++)
            {
                //characterRenderer = playerPreFab[i].GetComponent<SkinnedMeshRenderer>();
                playerPreFab[i].SetActive(i == characterPosition);
                characterCount++;
            }

            //characterRenderer.material = characterMaterials[materialPosition];
        }
        /*
        public void OnCharacterMaterialChangedUp()
        {
            
            materialPosition = (materialPosition + 1) % characterMaterials.Count;

            
            characterRenderer.material = characterMaterials[materialPosition];
        }
        
        public void OnCharacterMaterialChangedDown()
        {
            materialPosition = (materialPosition - 1) % characterMaterials.Count;

            characterRenderer.material = characterMaterials[materialPosition];
        }
        
        */

        public void OnChangeCharacterUp()
        {
            Debug.Log(characterPosition + " and Max count " + characterPosition);
            if(characterPosition == characterCount)
            {
                characterPosition = 0;
            }

            
            playerPreFab[characterPosition].SetActive(false);

            
            characterPosition = (characterPosition + 1) % playerPreFab.Count;

            
            playerPreFab[characterPosition].SetActive(true);
        }

        public void OnChangeCharacterDown()
        {
            
            if (characterPosition <= 0)
            {
                characterPosition = characterCount;
            }

            
            playerPreFab[characterPosition].SetActive(false);

            
            characterPosition = (characterPosition - 1) % playerPreFab.Count;

            
            playerPreFab[characterPosition].SetActive(true);
        }

        

        public object CaptureState()
        {
            return characterPosition;
        }

        public void RestoreState(object state)
        {
            characterPosition = (int)state; 
        }
    }
}

