using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.core
{
    public class EnableDisableCamMovment : MonoBehaviour
    {
        [SerializeField] private GameObject thirdPersonCam = null;




        public bool EnableDisable()
        {
            Debug.Log(thirdPersonCam.activeSelf + " cam current stats");

            if (thirdPersonCam != null)
            {
                thirdPersonCam.SetActive(!thirdPersonCam.activeSelf);
                return thirdPersonCam.activeSelf;
            }
            return false;
        }
    }

}
