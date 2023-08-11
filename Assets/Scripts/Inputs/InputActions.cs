using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Inputs
{
    public class InputActions : MonoBehaviour
    {
        MyInputs inputs;


        private void Awake()
        {
            inputs = new MyInputs();
        }

        private void OnEnable()
        {
            inputs.Enable();
        }

        private void OnDisable()
        {
            inputs.Disable();
        }

        public bool MovmentControl()
        {
            return inputs.PlayerActions.Move.IsPressed();
        }

        public Vector2 MousePosition()
        {
            return inputs.PlayerActions.mousePos.ReadValue<Vector2>();
        }

        //thirdPerson

        public Vector2 CharatcerMovement()
        {
            return inputs.PlayerActionsTheirdPerson.Movement.ReadValue<Vector2>();
        }

        public bool InteractWithComponet()
        {
            return inputs.PlayerActionsTheirdPerson.Interact.triggered;
        }

        public bool CharatcerJump()
        {
            return inputs.PlayerActionsTheirdPerson.Jump.triggered;
        }

        public bool CharacterBasicAttack()
        {
            return inputs.PlayerActionsTheirdPerson.BasicAttack.triggered;
        }

        public bool CharacterSprint()
        {
            return inputs.PlayerActionsTheirdPerson.Sprint.IsPressed();
        }

    }
}
