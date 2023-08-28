
using RPG.Attributes;
using RPG.Progression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {

        PlayerFighter fighter;
        BaseStats stats;
        [SerializeField] private GameObject healthUI = null;
        [SerializeField] private GameObject textHealthUI = null;
        

        private Slider healthSlider;
        private Text healthText;
        private float maxHealth;
        private GameObject enemyGameObject;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<PlayerFighter>();
            
            
            
            healthSlider = healthUI.GetComponent<Slider>();
            healthText = textHealthUI.GetComponent<Text>();
            
        }

        private void Start()
        {

        }

        private void Update()
        {
            //Debug.Log(fighter);
            if(fighter.GetTarget() == null)
            {
                healthUI.SetActive(false);
                return;
            }
            else
            {
                healthUI.SetActive(true);
                enemyGameObject = fighter.GetTarget().gameObject;
                stats = enemyGameObject.GetComponent<BaseStats>();
                maxHealth = stats.GetStat(Stats.Health);
                healthSlider.maxValue = maxHealth;
            }

            Health health = fighter.GetTarget();
            int currentHealthInt = Mathf.RoundToInt(health.CurrentHealth());


            healthText.text = $"{currentHealthInt}/{maxHealth}";
            healthSlider.value = health.CurrentHealth();
        }
    }
}




