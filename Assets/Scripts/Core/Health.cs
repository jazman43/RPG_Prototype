using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float currentHealth = 100f;
        [SerializeField] string deathAnimation = "die";

        private bool dead = false;

        public bool IsDead()
        {
            return dead;
        }

        public void TakeDamage(float damage)
        {
            float ranDamage = Random.Range(damage - 2, damage + 2);
            currentHealth = Mathf.Max(currentHealth - ranDamage, 0);
            Debug.Log(currentHealth + "< Health Damage >" + ranDamage);
            if (currentHealth == 0) {
                Die();
            }
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
    }

}
