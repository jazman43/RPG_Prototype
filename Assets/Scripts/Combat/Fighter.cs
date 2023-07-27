using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.core;



namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float damageToDo = 2f;


        [SerializeField]Health combatTarget;


        [SerializeField] private string punchAnimation = "punch";
        [SerializeField] private string stopAttack = "stopAttack";
        private float timeSinceLastAttack = Mathf.Infinity;

        
        Animator animator;

        ActionScheduler actionScheduler;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            

            if(combatTarget == null)
            {
                return;
            }

            if (combatTarget.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(combatTarget.transform.position, 1f);
                Debug.Log("moving to Target");
            }
            else 
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }            
        }

        private void AttackBehavior()
        {
            transform.LookAt(combatTarget.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                animator.ResetTrigger(stopAttack);
                animator.SetTrigger(punchAnimation);
                timeSinceLastAttack = 0;               
                
            }

            
        }

        public void Attack(GameObject target)
        {
            Debug.Log("Attacking");
            actionScheduler.StartAction(this);
            combatTarget = target.GetComponent<Health>();        
            
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, combatTarget.transform.position) < weaponRange;
        }

        public void Cancel()
        {
            animator.ResetTrigger(punchAnimation);
            animator.SetTrigger(stopAttack);
            combatTarget = null;

            GetComponent<Mover>().Cancel();
                            
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }


        //Anim        
        void Hit()
        {
            if (combatTarget == null) return;
            combatTarget.TakeDamage(damageToDo);
            Debug.Log("Punch!!");
        }
        void stopAnimation()
        {
            Cancel();
        }
    }
}

