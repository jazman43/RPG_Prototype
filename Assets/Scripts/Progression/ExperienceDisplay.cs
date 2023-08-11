using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RPG.Progression
{
    public class ExperienceDisplay : MonoBehaviour
    {
        
        BaseStats stats;
        Experice experice;
        
        [SerializeField] private GameObject xPBarUI = null;
        [SerializeField] private GameObject currentLevelUI = null;

        
        private Slider xpBarSlider;
        
        private Text currentLevelText;

        private void Awake()
        {
            experice = GameObject.FindWithTag("Player").GetComponent<Experice>();
            stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>(); 
            
            xpBarSlider = xPBarUI.GetComponent<Slider>();
            
            currentLevelText = currentLevelUI.GetComponent<Text>();
            
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            

            currentLevelText.text = stats.GetLevel().ToString();
            
            //Debug.Log(experice.GetExperience());
            xpBarSlider.value = experice.GetExperience();
            xpBarSlider.maxValue = experice.GetMaxXP();
        }
    }

}