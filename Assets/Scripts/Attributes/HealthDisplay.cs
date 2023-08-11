using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Progression;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        
        
        [SerializeField] private GameObject healthUI = null;
        [SerializeField] private GameObject textHealthUI = null;
        
        private Slider healthSlider;
        private Text healthText;
       
        

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
             
            healthSlider = healthUI.GetComponent<Slider>();
           
            healthText = textHealthUI.GetComponent<Text>();            
        }

        private void Start()
        {
                        
        }

        private void Update()
        {
            
            healthText.text = String.Format("{0:0}/{1:0}", health.CurrentHealth(), health.GetMaxHealthPoints());
             
            healthSlider.value = health.CurrentHealth();
            healthSlider.maxValue = health.GetMaxHealthPoints();
            
        }

        
    }

}