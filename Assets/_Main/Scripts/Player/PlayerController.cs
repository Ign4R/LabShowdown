using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerModel model;

    private InputActionAsset inputAsset;

    private InputActionMap player;

    private InputAction movement;

    private PlayerInput playerInput;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        model = GetComponent<PlayerModel>();
    }

    void Update()
    {
        model.Movement(movement.ReadValue<Vector2>().x);
        model.Jump();
        model.Timer();
        model.Raycasts();
    }

    private void OnEnable()
    {
        movement = player.FindAction("Movement");
        player.FindAction("Attack").performed += AttackInput;
        player.FindAction("Drop").performed += DropInput;
        player.FindAction("Jump").performed += JumpInput;
        player.FindAction("Jump").canceled += JumpInput;
        player.FindAction("AimUp").performed += AimUpInput;
        player.FindAction("AimUpRelease").performed += AimUpReleaseInput;
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Attack").performed -= AttackInput;
        player.FindAction("Drop").performed -= DropInput;
        player.FindAction("Jump").performed -= JumpInput;
        player.FindAction("Jump").canceled -= JumpInput;
        player.FindAction("AimUp").performed -= AimUpInput;
        player.FindAction("AimUpRelease").performed -= AimUpReleaseInput;
        player.Disable();
    }
 

    private void AttackInput(InputAction.CallbackContext context)
    {
        model.Attack();
    }
    private void DropInput(InputAction.CallbackContext context)
    {
        model.DropWeapon();
    }
    private void JumpInput(InputAction.CallbackContext context)
    { 
        

        if (context.canceled)
        {
            Debug.Log("me cancele");
            model.CancelledJump();
        }
        else
        {
            model.JumpQueue();
        }
    }

    private void AimUpInput(InputAction.CallbackContext context)
    {
        model.AimUp();
    }

    private void AimUpReleaseInput(InputAction.CallbackContext context)
    {
        model.AimUpRelease();
    }


}
