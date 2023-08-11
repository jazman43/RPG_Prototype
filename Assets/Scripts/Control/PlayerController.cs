
using UnityEngine;
using RPG.Inputs;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
               
        Health health;       


        [System.Serializable]
        struct CursorMapping
        {
            public Cursors cursorsType;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float raycastRadius = 1f;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;


        private void Awake()
        {     
            health = GetComponent<Health>();            
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


            if (InteractWithComponent()) return;
            
            
            GetComponent<PlayerMovement>().Movement();
            GetComponent<PlayerMovement>().JumpMovement();

        }

        

        private bool InteractWithUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
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

        

        private Ray GetMouseRay()
        {
            Vector3 pos = GetComponent<InputActions>().MousePosition();
            
            Vector3 mousePos = new Vector3(pos.x, pos.y, 0f);
            
            return Camera.main.ScreenPointToRay(mousePos);
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

    }
}

