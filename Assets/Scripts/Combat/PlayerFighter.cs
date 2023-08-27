using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using System;
using RPG.core;
using Jareds.Utils;
using RPG.Inventories;

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
            GetClosetedTarget();
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
                    return targets;
                }
            }
            return null;
        }

        public void RemoveFoundCombatTagets()
        {
            List<Health> targetsToRemove = new List<Health>();

            foreach (Health target in targets)
            {
                float distanceToTarget = Vector3.Distance(
                    transform.position,
                    target.transform.position);
                
                //if i try and remove the target directly i get an error
                if(distanceToTarget > sphereCastRadius)
                {
                    targetsToRemove.Add(target);
                }
            }

            foreach (Health targetToRemove in targetsToRemove)
            {
                targets.Remove(targetToRemove);
            }
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
            //animator.ResetTrigger(punchAnimation);
            //animator.SetTrigger(stopAttack);
            combatTarget = null;
        }
    }
}

