using RPG.Inputs;
using UnityEngine;
using RPG.Attributes;
using RPG.Control;


namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        /*
        public Cursors GetCursorType()
        {
            return Cursors.Attack;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false;

            Debug.Log(callingController.GetComponent<PlayerFighter>().CanAttack(gameObject) + " Can Attack?");
            
            
            if (!callingController.GetComponent<PlayerFighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (GetComponent<InputActions>().CharacterBasicAttack())
            {
                callingController.GetComponent<PlayerFighter>().Attack(gameObject);
            }
            
            return true;
        }
        */
    }
}

