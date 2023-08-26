using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private bool isHoming = false;
        [SerializeField] private GameObject hitEffect = null;
        
        [SerializeField] Health combatTarget = null;
        private Vector3 targetPoint;
        [SerializeField] private float damage = 0;
        [SerializeField] private GameObject[] destroyObject = null;
        [SerializeField] UnityEvent onHit;

        GameObject instigator = null;

        private void Start()
        {            
           transform.LookAt(GetShootAtPos()); 
        }

        private void Update()
        {
            if (combatTarget != null && isHoming && !combatTarget.IsDead())
            {
                transform.LookAt(GetShootAtPos());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, target);
        }

        public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, null, targetPoint);
        }


        public void SetTarget(GameObject instigator, float damage, Health target = null, Vector3 targetPoint = default)
        {
            this.combatTarget = target;
            this.targetPoint = targetPoint;
            this.damage = damage;
            this.instigator = instigator;

            
            Destroy(gameObject, 5f);
        }

        

        private Vector3 GetShootAtPos()
        {
            if(combatTarget == null)
            {
                return targetPoint;
            }

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
            
            if (combatTarget != null && health != combatTarget) return;
            if (health == null || health.IsDead()) return;
            if (other.gameObject == instigator) return;

            health.TakeDamage(instigator ,damage);

            speed = 0;

            onHit.Invoke();

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetShootAtPos(), transform.rotation);
            }
            
            foreach(GameObject toDestroy in destroyObject)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, 0.5f);
            
        }
    }
}

