using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inputs;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.core;
using UnityEngine.Windows;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        [Header("Animation")]
        [SerializeField] private string animSpeed = "forwardSpeed";
        Animator animator;

        MyInputs myPlayerInputs;
        Health health;
        Mover mover;


        private void Awake()
        {
            //point and click
            myPlayerInputs = new MyInputs();

            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }

        private void OnEnable()
        {
            myPlayerInputs.Enable();
        }

        private void OnDisable()
        {
            myPlayerInputs.Disable();
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (InteractWithCombat())
            {
                return;
            }

            if (InteractWithMovement()) return;

            Debug.Log("nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;


                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) { continue; }

                if (myPlayerInputs.PlayerActions.Move.IsInProgress())
                {
                    GetComponent<Fighter>().Attack(target.gameObject);

                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {

            RaycastHit hitInfo;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);

            if (hasHit)
            {
                
                if (myPlayerInputs.PlayerActions.Move.IsInProgress())
                {
                    mover.StartMoveAction(hitInfo.point, 1f);
                }
                return true;
            }
            return false;
        }

        private Ray GetMouseRay()
        {
            Vector3 pos = myPlayerInputs.PlayerActions.mousePos.ReadValue<Vector2>();
            //Debug.Log(pos);
            Vector3 mousePos = new Vector3(pos.x, pos.y, 0f);
            //                                  //                  //
            return Camera.main.ScreenPointToRay(mousePos);
        }

    }
}

