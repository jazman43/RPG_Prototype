using RPG.Saving;
using RPG.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Progression;
using Jareds.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        //float currentHealth =  -1f;
        [SerializeField] float regenerationPercentage = 100;
        [SerializeField] string deathAnimation = "die";
        [SerializeField] TakeDamageEvent takeDam;
        public UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyJaredValue<float> currentHealth;
        private bool dead = false;

        private void Awake()
        {
            currentHealth = new LazyJaredValue<float>(GetInitiHealth);
        }

        private float GetInitiHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stats.Health);
        }

        private void Start()
        {
            currentHealth.ForceInit();
            
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return dead;
        }

       

        public void TakeDamage(GameObject instigator , float damage)
        {

            Debug.Log(gameObject.name + " damaged: " + damage);

            float ranDamage = UnityEngine.Random.Range(damage - 2, damage + 2);
            currentHealth.value = Mathf.Max(currentHealth.value - ranDamage, 0);
            Debug.Log(currentHealth + "< Health Damage >" + ranDamage + " in " + instigator);
            if (currentHealth.value == 0) {
                onDie.Invoke();
                Die();
                GiveXP(instigator);
            }
            else
            {
                takeDam.Invoke(ranDamage);
            }
        }

        

        public float CurrentHealth()
        {
            return currentHealth.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stats.Health);
        }

        private void GiveXP(GameObject instigator)
        {

            Experice experience = instigator.GetComponent<Experice>();
            Debug.Log(experience + "insterdater");
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stats.ExperienceReward));
        }

        public void Die()
        {
            if (IsDead())
            {
                Debug.Log("death");
                return;
                
            }
            dead = true;
            GetComponent<Animator>().SetTrigger(deathAnimation);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void Heal(float healthToRestore)
        {
            currentHealth.value = Mathf.Min(currentHealth.value + healthToRestore, GetMaxHealthPoints());

        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stats.Health) * (regenerationPercentage / 100);
            currentHealth.value = Mathf.Max(currentHealth.value, regenHealthPoints);            
        }

        public object CaptureState()
        {
            return currentHealth.value;
        }

        public void RestoreState(object state)
        {
            currentHealth.value = (float)state;

            if(currentHealth.value <= 0)
            {
                Die();
            }            
            
        }
    }

}
