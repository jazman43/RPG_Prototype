using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
using RPG.core;
using RPG.Combat;
using MyRPG.Control;

namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float supicionTime = 6f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float wayPointWait = 6f;
        [SerializeField][Range(0, 1)] float patrolSpeedFraction = 0.2f;

        private NavMeshAgent agent;
        private GameObject player;
        private Mover mover;
        private Fighter fighter;
        private Health health;

        private Vector3 guardPos;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWayPointIndex = 0;
        float timeSinceLastWayPoint = Mathf.Infinity;


        private void Awake()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            agent = GetComponent<NavMeshAgent>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");

        }

        private void Start()
        {
            guardPos = transform.position;
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (IsInRangeToChace() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
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
        }

        private bool IsInRangeToChace()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPos;

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
            fighter.Attack(player);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

