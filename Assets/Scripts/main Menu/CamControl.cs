using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Menus
{
    public class CamControl : MonoBehaviour
    {

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private CinemachineTrackedDolly dollyCart;

        private float delayTime = 5f;
        private float resetThreshold = 0.8f;

        private void Start()
        {
            dollyCart = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

        private void FixedUpdate()
        {
            
            if (dollyCart.m_PathPosition > resetThreshold)
            {                
                StartCoroutine(WaitForCam());
            }
        }

        IEnumerator WaitForCam()
        {
            // Wait for the specified delay before continuing
            yield return new WaitForSeconds(delayTime);

            // Reset pathPosition to 0 after the delay
            dollyCart.m_PathPosition = 0;
            Debug.Log("Camera path position reset after " + delayTime + " seconds.");
        }
    }
}

