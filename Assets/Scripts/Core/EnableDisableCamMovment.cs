using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.core
{
    public class EnableDisableCamMovment : MonoBehaviour
    {
        [SerializeField] private GameObject thirdPersonCam = null;




        public void EnableDisable()
        {
            Debug.Log(thirdPersonCam.activeSelf);

            if (thirdPersonCam != null)
            {
                thirdPersonCam.SetActive(!thirdPersonCam.activeSelf);
            }
        }
    }

}
