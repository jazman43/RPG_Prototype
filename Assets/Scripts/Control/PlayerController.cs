
using UnityEngine;
using RPG.Inputs;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System.Collections.Generic;
using RPG.Inventories;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
               
        Health health;
        InputActions input;
        ActionStore actionStore;

        [System.Serializable]
        struct CursorMapping
        {
            public Cursors cursorsType;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        bool isDraggingUI = false;

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float raycastRadius = 1f;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;


        private List<CombatTarget> targets = new List<CombatTarget>();
        //private Camera camera;
        

        private void Awake()
        {
            actionStore = GetComponent<ActionStore>();
            health = GetComponent<Health>();
            input = GetComponent<InputActions>();
        }

        private void Start()
        {
            //camera = Camera.main;
        }

        private void Update()
        {
            if (InteractWithUI())
            {
                SetCursors(Cursors.UI);
                return;
            }

            if (health.IsDead())
            {
                SetCursors(Cursors.OutOfBounds);
                return;
            }

            UseAbilities();

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            SetCursors(Cursors.OutOfBounds);

        }

        

        private bool InteractWithUI()
        {
            if (!input.MovmentControl())
            {
                isDraggingUI = false;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (input.MovmentControl())
                {
                    isDraggingUI = true;
                }
                SetCursors(Cursors.UI);
                return true;
            }
            if(isDraggingUI)
            {
                return true;
            }

            return false;
        }


        private void UseAbilities()
        {
            if (input.GetActions1())
            {
                actionStore.Use(0, gameObject);
                Debug.Log("using 1" + gameObject);
            }
            if (input.GetActions2())
            {
                actionStore.Use(1, gameObject);
                Debug.Log("using 2" + gameObject);
            }
            if (input.GetActions3())
            {
                actionStore.Use(2, gameObject);
                Debug.Log("using 3" + gameObject);
            }
            if (input.GetActions4())
            {
                actionStore.Use(3, gameObject);
                Debug.Log("using 4" + gameObject);
            }

        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();

                foreach(IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursors(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;

        }


        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false;

                if (input.MovmentControl())
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursors(Cursors.Move);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            return true;
        }


        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        

        private void SetCursors(Cursors cursors)
        {
            CursorMapping mapping = GetCursorMapping(cursors);

            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(Cursors type)
        {
            foreach(CursorMapping mapping in cursorMappings)
            {
                if(mapping.cursorsType == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        /*
        private void GetTargets()
        {
            if (targets.Count == 0) return;

            CombatTarget closetTarget = null;
            float closestTargetDis = Mathf.Infinity;


            foreach(CombatTarget target in targets)
            {
                Vector2 viewPos = camera.WorldToViewportPoint(target.transform.position);


                if (!target.GetComponentInChildren<Renderer>().isVisible) continue;

                Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
                if(toCenter.sqrMagnitude < closestTargetDis)
                {
                    closetTarget = target;
                    closestTargetDis = toCenter.sqrMagnitude;
                }
            }

            

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<CombatTarget>(out CombatTarget target)) return;

            targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<CombatTarget>(out CombatTarget target)) return;

            targets.Remove(target);
        }
        */
    }
}

