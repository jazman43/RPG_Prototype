using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.core;
using System;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private bool isHoming = false;
        [SerializeField] private GameObject hitEffect = null;
        
        [SerializeField] Health combatTarget = null;
        [SerializeField] private float damage = 0;
        

        private void Start()
        {
            if (!isHoming)
            {
                transform.LookAt(GetShootAtPos());
            }
            
        }

        public void SetTarget(Health target, float damageTarget)
        {
            this.combatTarget = target;
            this.damage = damageTarget;


            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            if (combatTarget == null ) return;
            if (isHoming && !combatTarget.IsDead())
            { 
                transform.LookAt(GetShootAtPos());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetShootAtPos()
        {
            
            CapsuleCollider targetCapsule = combatTarget.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return combatTarget.transform.position;
            }
            return combatTarget.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        

        private void OnTriggerEnter(Collider other)
        {

            Health health = other.GetComponent<Health>();
            Debug.Log(health + " hello " + other);
            if (combatTarget != null && health != combatTarget) return;
            if (health == null || health.IsDead()) return;

            combatTarget.TakeDamage(damage);

            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetShootAtPos(), transform.rotation);
            }
            

            Destroy(gameObject);
            
        }
    }
}

