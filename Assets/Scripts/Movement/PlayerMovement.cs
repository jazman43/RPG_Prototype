using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inputs;
using RPG.Control;
using RPG.Attributes;

namespace RPG.Movement
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 0;
        [SerializeField] private float jumpForece = 1f;
        [SerializeField] private float doubleJump = 1.5f;
        [SerializeField] private int maxDoubleJump = 2;
        [SerializeField] private float sprintSpeed = 6f;
        [SerializeField] private float walkSpeed = 3.5f;
        [SerializeField] private float turnSmoothtime = .1f;

        

        [SerializeField] private float rotationSpeed = 3f;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Cinemachine.CinemachineFreeLook cinemachineFreeLook;


        [SerializeField] private string animationSpeed = "forwardSpeed";

        
        private CharacterController controller;
        private Vector3 YPos;
        private float griavity = -9.81f;
        private bool isGrounder = false;        
        private float turnSmoothVel;
        private Transform cam;        
        private Vector3 velocity;        
        private bool isJumping;
        private int jumpsRemaining;

        InputActions inputs;
        Health health;
        Animator animator;

        private void Awake()
        {            
            controller = GetComponent<CharacterController>();
            inputs = GetComponent<InputActions>();
            health = GetComponent<Health>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            jumpsRemaining = maxDoubleJump;
            cam = Camera.main.transform;
        }


        

        private void LateUpdate()
        {
            Gravitiy();

        }

        public void Movement()
        {
            if (health.IsDead()) return;

            Vector2 move = inputs.CharatcerMovement();

            Debug.Log(move + "im Moveing In this Dir");




            if (inputs.CharacterSprint())
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }

            float trueSpeed = move.magnitude * moveSpeed;

            if (move.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothtime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

                
            }
            Debug.Log(trueSpeed);

            animator.SetFloat(animationSpeed, trueSpeed);

        }

        private void Gravitiy()
        {
            if (isGrounder && YPos.y < 0)
            {
                YPos.y = 0f;

            }

            Debug.Log(isGrounder + " is grounded");

            YPos.y += griavity * Time.deltaTime;
            controller.Move(YPos * Time.deltaTime);
        }

        public void JumpMovement()
        {
            if (health.IsDead()) return;

            if (isGrounder)
            {
                isJumping = false;
                jumpsRemaining = maxDoubleJump;
            }

            if (inputs.CharatcerJump())
            {
                if (isGrounder)
                {
                    isJumping = true;
                    YPos.y += Mathf.Sqrt(jumpForece * -3f * griavity);
                }
                else if (jumpsRemaining > 0)
                {
                    isJumping = true;
                    YPos.y += Mathf.Sqrt(doubleJump * -3f * griavity);
                    jumpsRemaining--;
                    Debug.Log("next jump");

                }
            }
        }       


        private void FixedUpdate()
        {
            
        }
        private void Update()
        {
            isGrounder = controller.isGrounded;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}

