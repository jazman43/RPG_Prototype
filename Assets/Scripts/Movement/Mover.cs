using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] private Transform target;
        [SerializeField] private float maxSpeed = 4f;

        [Header("Animation")]
        [SerializeField] private string animSpeed = "forwardSpeed";

        private NavMeshAgent playerControler;

        private Health health;

        private Animator animator;



        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerControler = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }


        private void Update()
        {
            playerControler.enabled = !health.IsDead();
            UpDateAnimator();
        }

        public void StartMoveAction(Vector3 destiation, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            MoveTo(destiation, speedFraction);
        }

        public void MoveTo(Vector3 destiation, float speed)
        {
            playerControler.SetDestination(destiation);
            playerControler.speed = maxSpeed * Mathf.Clamp01(speed);
            playerControler.isStopped = false;
        }


        private void UpDateAnimator()
        {
            Vector3 velocity = playerControler.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);

            float speed = localVel.z;

            animator.SetFloat(animSpeed, speed);
        }

        public void Cancel()
        {
            playerControler.isStopped = true;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();

            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }
    }


    
}

