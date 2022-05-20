using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class TwinStickMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadZone = 0.11f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    public Animator anim;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;
    private bool isJump = false;
    private bool isGrounded = false;

    [SerializeField] private float jump;
    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;


    private PlayerControls playerControls;
    private PlayerInput playerInput;

    public static bool isTopDown = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();

    }

    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
        
        if (!isJump) 
        jump = playerControls.Controls.Jump.ReadValue<float>();
    }

    void HandleMovement()
    {
        Vector3 move;

        if (isTopDown)
        {

            //take off jump from here

            if (jump > 0)
                isJump = true;
            else
                isJump = false;

            move = new Vector3(movement.x, jump, movement.y);
            anim.SetBool("IsJump", isJump);
        }
        else
        {
            if (jump > 0)
                isJump = true;
            else
                isJump = false;

            move = new Vector3(movement.x, jump, 0);
            anim.SetBool("IsJump", isJump);
        }

       
        controller.Move(move * Time.deltaTime * playerSpeed);
        anim.SetBool("IsRun", move.magnitude > 0);


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        isGrounded = false;

        if (controller.isGrounded)
        {
            playerVelocity.y = 0; 
            isJump = false;
            isGrounded = true;
        }

        anim.SetFloat("yVelocity", playerVelocity.y);
        anim.SetBool("IsGround", isGrounded);
    }

    void HandleRotation()
    {
        if (isGamepad)
        {
            if(Mathf.Abs(aim.x) > controllerDeadZone || Mathf.Abs(aim.y) > controllerDeadZone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;

                if(playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            //Point on screen from ray
            if (isTopDown)
            {
                Ray ray = Camera.main.ScreenPointToRay(aim);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;

                if (groundPlane.Raycast(ray, out rayDistance))
                {
                    Vector3 point = ray.GetPoint(rayDistance);
                    LookAt(point);
                }
            }
            else
            {
                Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

                Vector3 heightCorrectedPoint = new Vector3(Camera.main.ScreenToWorldPoint(mouseScreenPosition).x, transform.position.y, transform.position.z);

                transform.LookAt(heightCorrectedPoint, Vector3.up);
            }
        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

}
