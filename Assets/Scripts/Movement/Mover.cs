using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.core;
using RPG.Saving;
using RPG.Attributes;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] private Transform target;
        [SerializeField] private float maxSpeed = 4f;
        [SerializeField] float maxNavPathLength = 40f;

        [Header("Animation")]
        [SerializeField] private string animSpeed = "forwardSpeed";

        private NavMeshAgent aiAgent;

        private Health health;

        private Animator animator;



        private void Awake()
        {
            animator = GetComponent<Animator>();
            aiAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }


        private void Update()
        {
            //playerControler.enabled = !health.IsDead();
            UpDateAnimator();
        }

        public void StartMoveAction(Vector3 destiation, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            MoveTo(destiation, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        public void MoveTo(Vector3 destiation, float speed)
        {
            aiAgent.SetDestination(destiation);
            aiAgent.speed = maxSpeed * Mathf.Clamp01(speed);
            aiAgent.isStopped = false;
        }


        private void UpDateAnimator()
        {
            Vector3 velocity = aiAgent.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);

            float speed = localVel.z;

            animator.SetFloat(animSpeed, speed);
        }

        public void Cancel()
        {
            aiAgent.isStopped = true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
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

