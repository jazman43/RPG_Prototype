using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using System;
using RPG.core;
using Jareds.Utils;
using RPG.Inventories;
using RPG.Progression;
using RPG.Inputs;
using RPG.Dialogue;
using RPG.Shops;

namespace RPG.Combat
{
    public class PlayerFighter : MonoBehaviour, IAction
    {
        /*
         * get combat targets by ray cast spheare add each to a list 
         * get closest change camera aim at to closest get key press to change target
         * 
         * 
         * How Can we do damge currently damge is done with a call to the animation Hit() 
         * event function this works if the player is in range but can we do a check and 
         * what type of check? 
         * 
         */
        [SerializeField] private float sphereCastRadius = 10f;        
        [SerializeField] private Vector3 sphereCastDirection;
        [SerializeField] private Weapon defaultWeapon = null;

        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;


        [SerializeField] private List<Health> targets = new List<Health>();

        [Header("Animation")]
        [SerializeField] private string punchAnimation = "punch";
        [SerializeField] private string stopAttack = "stopAttack";

        private float timeSinceLastAttack = Mathf.Infinity;


        Camera camera;
        Health combatTarget;
        ActionScheduler actionScheduler;
        Weapon currentWeaponCofig;
        Equipment equipment;
        LazyJaredValue<OnWeaponEquipment> currentWeapon;
        Animator animator;
        

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

        private void Start()
        {
            currentWeapon.ForceInit();
            camera = Camera.main;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (combatTarget.GetComponent<DialogueTrigger>() != null) return;

            if (GetComponent<InputActions>().CharacterBasicAttack())
            {
                if (combatTarget != null && CanAttack(combatTarget.gameObject))
                {
                    AttackBehavior();
                }
            }
            
            GetClosetedTarget();

            ClearTargetsList();
        }

        private OnWeaponEquipment SetupDefaultWeapon()
        {

            return AttachWeapon(defaultWeapon);
        }


        public void Attack(GameObject gameObject)
        {
            actionScheduler.StartAction(this);
            combatTarget = gameObject.GetComponent<Health>();
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
           

            //get attack range check if player is faceing then attack


            if (!GetIsInRange(target.transform)) return false;

            /*
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
            */
            //is target in front of us?


            Vector3 directionToTarget = target.transform.position - transform.position;
            directionToTarget.y = 0;

            if (Vector3.Dot(directionToTarget.normalized, transform.forward) <= 0)
            {
                return false;
            }


            RaycastHit hit;
            if(Physics.Raycast(transform.position, directionToTarget, out hit, currentWeaponCofig.GetWeaponRange()))
            {
                Debug.Log(hit + " object hit");
                if (hit.collider.gameObject == target)
                {
                    Health targetToTest = target.GetComponent<Health>();

                    return targetToTest != null && !targetToTest.IsDead();
                }
            }
            
            return false;

        }


        private void AttackBehavior()
        {            
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

        //ray sphere 

        private List<Health> FindCombatTargets()
        {
            

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereCastRadius, sphereCastDirection);

           

            foreach (RaycastHit hit in hits)
            {
                Health hitObject = hit.collider.gameObject.GetComponent<Health>();


                
                if (hitObject != null && !targets.Contains(hitObject) && hitObject.tag != "Player")
                {
                    targets.Add(hitObject);
                }
            }
            return targets;
        }

        private void ClearTargetsList()
        {
            targets.Clear();
        }

        private Health GetClosetedTarget()
        {
            float closestDistance = Mathf.Infinity;
            foreach(Health target in FindCombatTargets())
            {
                if(GetCombatTargetInRange(target.transform) < closestDistance)
                {
                    closestDistance = GetCombatTargetInRange(target.transform);
                    combatTarget = target;
                }
            }
            return combatTarget;
        }

        public Health GetTarget()
        {
            Debug.Log(combatTarget);            
            return combatTarget;
        }

        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponCofig.GetWeaponRange();
        }

        private float GetCombatTargetInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) ;
        }

        private OnWeaponEquipment AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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

        private void UpdateWeapon()
        {
            Weapon weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as Weapon;
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                Debug.Log("weapon Equiped" + weapon);
                EquipWeapon(weapon);
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeaponCofig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        public void Cancel()
        {
            animator.ResetTrigger(punchAnimation);
            animator.SetTrigger(stopAttack);
            combatTarget = null;
        }

        //animation event trigger
        void Hit()
        {
            if (combatTarget == null) return;

            float damage = GetComponent<BaseStats>().GetStat(Stats.Damage);
            BaseStats targetBaseStats = combatTarget.GetComponent<BaseStats>();
            if (targetBaseStats != null)
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
                Debug.Log("player Doing damege to " + combatTarget);
                combatTarget.TakeDamage(gameObject, damage);
            }
        }

        void stopAnimation()
        {
            Cancel();
        }

    }
}

