using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Menus
{
    public class CamControl : MonoBehaviour
    {

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float camSpeed = 1.0f;
        [SerializeField] private bool canReset = false;
        [SerializeField]private GameObject uiMenuToggles = null;


        private CinemachineTrackedDolly dollyCart;

        private void Awake()
        {
            dollyCart = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

      

        private void FixedUpdate()
        {
            
           OnNewGame();
        }

        private void OnNewGame()
        {
            dollyCart.m_PathPosition += camSpeed * Time.deltaTime;
            //Debug.Log(dollyCart.m_PathPosition);
            if(canReset)
            {
                if(dollyCart.m_PathPosition > .9f && uiMenuToggles.activeSelf != true)
                {
                    dollyCart.m_PathPosition = 0;
                }
                
            }

        }
    }
}

