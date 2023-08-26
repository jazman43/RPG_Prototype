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
        
        [SerializeField] float regenerationPercentage = 100;
        [SerializeField] string deathAnimation = "die";
        [SerializeField] TakeDamageEvent takeDam;
        public UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyJaredValue<float> currentHealth;
        bool wasDeadLastFrame = false;

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
            return currentHealth.value <= 0;
        }

       

        public void TakeDamage(GameObject instigator , float damage)
        {

            Debug.Log(gameObject.name + " damaged: " + damage);

            float ranDamage = UnityEngine.Random.Range(damage - 2, damage + 2);
            currentHealth.value = Mathf.Max(currentHealth.value - ranDamage, 0);
            Debug.Log(currentHealth + "< Health Damage >" + ranDamage + " in " + instigator);
            if (IsDead()) {
                onDie.Invoke();                
                GiveXP(instigator);
                Destroy(gameObject, 15);
            }
            else
            {
                takeDam.Invoke(ranDamage);
            }
            UpdateState();
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

        

        public void Heal(float healthToRestore)
        {
            currentHealth.value = Mathf.Min(currentHealth.value + healthToRestore, GetMaxHealthPoints());
            UpdateState();
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stats.Health) * (regenerationPercentage / 100);
            currentHealth.value = Mathf.Max(currentHealth.value, regenHealthPoints);            
        }

        private void UpdateState()
        {
            Animator animator = GetComponent<Animator>();
            if (!wasDeadLastFrame && IsDead())
            {
                animator.SetTrigger(deathAnimation);
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }

            if (wasDeadLastFrame && !IsDead())
            {
                animator.Rebind();
            }

            wasDeadLastFrame = IsDead();
        }

        public object CaptureState()
        {
            return currentHealth.value;
        }

        public void RestoreState(object state)
        {
            currentHealth.value = (float)state;

            UpdateState();         
            
        }
    }

}
