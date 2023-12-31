using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.core;
using RPG.Saving;
using System;
using RPG.Attributes;
using RPG.Progression;
using Jareds.Utils;
using RPG.Inventories;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("weapon config")]
        [SerializeField] private float timeBetweenAttacks = 1f;        
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        [SerializeField] private Weapon defaultWeapon = null;
        
        

        [Header("Animation")]
        [SerializeField] private string punchAnimation = "punch";
        [SerializeField] private string stopAttack = "stopAttack";


        private float timeSinceLastAttack = Mathf.Infinity;

        LazyJaredValue<OnWeaponEquipment> currentWeapon;
        Weapon currentWeaponCofig;
        Animator animator;
        Health combatTarget;
        Equipment equipment;
        ActionScheduler actionScheduler;

        private void Awake()
        {
            currentWeaponCofig = defaultWeapon;
            currentWeapon = new LazyJaredValue<OnWeaponEquipment>(SetupDefaultWeapon);
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
                //EquipWeapon(defaultWeapon);
            }
        }

        

        private OnWeaponEquipment SetupDefaultWeapon()
        {
            
            return AttachWeapon(defaultWeapon);
        }

        private void Start()
        {
            currentWeapon.ForceInit();            
        }

        

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            

            if(combatTarget == null)
            {
                return;
            }

            if (combatTarget.IsDead())
            {
                return;               
            }


            if (!GetIsInRange(combatTarget.transform))
            {
                GetComponent<Mover>().MoveTo(combatTarget.transform.position, 1f);
                Debug.Log("moving to Target " + combatTarget);
            }
            else 
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }            
        }


        public void EquipWeapon(Weapon weapon)
        {
            currentWeaponCofig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        private void UpdateWeapon()
        {
            Weapon weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as Weapon;
            if(weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                Debug.Log("weapon Equiped" + weapon);
                EquipWeapon(weapon);
            }
        }


        private OnWeaponEquipment AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return combatTarget;
        }

        public Transform GetHandTransform(bool isRightHand)
        {
            if (isRightHand)
            {
                return rightHandTransform;
            }
            else
            {
                return leftHandTransform;
            }
        }


        private void AttackBehavior()
        {
            transform.LookAt(combatTarget.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;               
                
            }            
        } 

        private void TriggerAttack()
        {
            animator.ResetTrigger(stopAttack);
            animator.SetTrigger(punchAnimation);
        }

        public void Attack(GameObject target)
        {
            Debug.Log("Attacking");
            actionScheduler.StartAction(this);
            combatTarget = target.GetComponent<Health>();        
            
        }

        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponCofig.GetWeaponRange();
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
            if (!GetComponent<Mover>().CanMoveTo(combatTarget.transform.position) &&
                !GetIsInRange(combatTarget.transform))
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

            float damage = GetComponent<BaseStats>().GetStat(Stats.Damage);
            BaseStats targetBaseStats = combatTarget.GetComponent<BaseStats>();
            if(targetBaseStats != null)
            {
                float defence = targetBaseStats.GetStat(Stats.defence);

                damage /= 1 + defence / damage;
            }
            

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }


            if (currentWeaponCofig.HasProjectile())
            {
                currentWeaponCofig.SpawnProjectile(rightHandTransform, leftHandTransform, combatTarget, gameObject, damage);
            }
            else
            {                
                combatTarget.TakeDamage(gameObject, damage);
            }           
        }
        void stopAnimation()
        {
            Cancel();
        }



    }
}

