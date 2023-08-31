using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
using RPG.core;
using RPG.Combat;
using RPG.Attributes;
using Jareds.Utils;


namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float supicionTime = 6f;
        [SerializeField] float agroCooldownTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float wayPointWait = 6f;
        [SerializeField] float shoutDistance = 5f;
        [SerializeField][Range(0, 1)] float patrolSpeedFraction = 0.2f;

        private NavMeshAgent agent;
        private GameObject player;
        private Mover mover;
        private Fighter fighter;
        private Health health;

        private LazyJaredValue<Vector3> guardPos;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWayPointIndex = 0;
        float timeSinceLastWayPoint = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;


        private void Awake()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            agent = GetComponent<NavMeshAgent>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");

            guardPos = new LazyJaredValue<Vector3>(GetGuardPosition);
            guardPos.ForceInit();

        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
           
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            //just for npc we might want a debug log here in case we accidantly forget the fighter on an enemy
            if (fighter == null) return;
            
            if (IsAggrevated() && fighter.CanAttack(player))
            {
                
                AttackPlayer();
                //agent.speed = 3.5f;
            }
            else if (timeSinceLastSawPlayer < supicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                //agent.speed = 1.5f;
                PatrolBehavior();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceLastWayPoint += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;
        }

        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }


       
        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPos.value;

            if (patrolPath != null)
            {
                if (AtWayPoit())
                {
                    timeSinceLastWayPoint = 0f;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if (timeSinceLastWayPoint > wayPointWait)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }

        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWayPoit()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance;
        }


        private void AttackPlayer()
        {
            timeSinceLastSawPlayer = 0;

            fighter.Attack(player);

            AggrevateNearbyEnemies();
        }

        private void AggrevateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            foreach (RaycastHit hit in hits)
            {
                AiController ai = hit.collider.GetComponent<AiController>();
                if (ai == null) continue;

                ai.Aggrevate();
            }
        }

        private bool IsAggrevated()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

