using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Movement
{
    public class PlayerMover : MonoBehaviour, ISaveable
    {
        /*
            star with basic movement with a sprint 

            no jump yet
         */
        [SerializeField] private float walkSpeed = 2.5f;
        [SerializeField] private float sprintSpeed = 6.5f;
        [SerializeField] private float turnSmoothTime = 0.1f;


        [Header("Animation")]
        [SerializeField] private string animSpeed = "forwardSpeed";

        private float moveSpeed = Mathf.Infinity;
        
        private Vector3 yPosition;
        private float griavity = -9.81f;
        private bool isGrounded = true;
        private float turnSmoothVelocity;

        private CharacterController controller;
        private Animator animator;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;
            //Debug.Log(isGrounded);
        }

        public void Movement(Vector3 moveControl, bool isInSprint, Camera camera)
        {
            

            if (isInSprint)
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }

            if(moveControl.magnitude <= 0.01f)
            {
                moveSpeed = 0;
            }

            if(moveControl.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(
                    moveControl.x,
                    moveControl.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    targetAngle,
                    ref turnSmoothVelocity,
                    turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

                

                
            }
            animator.SetFloat(animSpeed, moveSpeed);
            Debug.Log(moveControl.magnitude);
        }


        public void Gravity()
        {
            if(isGrounded && yPosition.y < 0)
            {
                yPosition.y = 0f;
            }

            yPosition.y += griavity * Time.deltaTime;
            controller.Move(yPosition * Time.deltaTime);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;            
            transform.position = position.ToVector();
            
        }
    }
}

