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

        public bool ShowUI()
        {
            return inputs.PlayerActions.ShowUI.triggered;
        }

        public bool GetActions1()
        {
            return inputs.PlayerActions.ActionBar1.triggered;
        }

        public bool GetActions2()
        {
            return inputs.PlayerActions.ActionBar2.triggered;
        }
        public bool GetActions3()
        {
            return inputs.PlayerActions.ActionBar3.triggered;
        }
        public bool GetActions4()
        {
            return inputs.PlayerActions.ActionBar4.triggered;
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

        public Vector2 Look()
        {
            return inputs.PlayerActionsTheirdPerson.CamraMovment.ReadValue<Vector2>();
            
        }

    }
}
