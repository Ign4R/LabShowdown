using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("ENABLE ONLY TO TEST")]

    [SerializeField] private bool canTest;

    private SpriteRenderer skin;

    private Material outline;

    private PlayerModel model;

    private InputActionAsset inputAsset;

    private InputActionMap player;

    private InputAction movement;

    private PlayerInput playerInput; 

    private InputAction attack;

    public PlayerConfiguration PlayerConfig { get; private set; }

    private void Awake()
    {
        skin = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if(canTest) Test();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        PlayerConfig = pc;
        skin.sprite = pc.PlayerSkin;
        Material temp = new Material(skin.material.shader);
        skin.material = temp;
        skin.material.SetColor("_SolidOutline", new Color(pc.SkinColor.r, pc.SkinColor.g, pc.SkinColor.b));
        playerInput = PlayerConfig.Input;
        inputAsset =PlayerConfig.Input.actions;
        player = inputAsset.FindActionMap("Player");
        model = GetComponent<PlayerModel>();
        attack = player.FindAction("Attack");
        movement = player.FindAction("Movement");
        player.FindAction("Drop").performed += DropInput;
        player.FindAction("Jump").performed += JumpInput;
        player.FindAction("Jump").canceled += JumpInput;
        player.FindAction("AimUp").performed += AimUpInput;
        player.FindAction("AimUpRelease").performed += AimUpReleaseInput;
        player.Enable();
    }

    void Update()
    {
        model.Movement(movement.ReadValue<Vector2>().x);
        model.Attack(attack.ReadValue<float>());
        model.Jump(movement.ReadValue<Vector2>().x);
        model.LimitHeight();
        model.Timer();
        model.Raycasts();
    }

    private void OnDisable()
    {
        player.FindAction("Drop").performed -= DropInput;
        player.FindAction("Jump").performed -= JumpInput;
        player.FindAction("Jump").canceled -= JumpInput;
        player.FindAction("AimUp").performed -= AimUpInput;
        player.FindAction("AimUpRelease").performed -= AimUpReleaseInput;
        player.Disable();
    }

    public void Test()
    {
        PlayerConfig = new PlayerConfiguration(playerInput);
        model = GetComponent<PlayerModel>();
        playerInput = PlayerConfig.Input;
        inputAsset = PlayerConfig.Input.actions;
        player = inputAsset.FindActionMap("Player");
        model = GetComponent<PlayerModel>();
        attack = player.FindAction("Attack");
        movement = player.FindAction("Movement");
        player.FindAction("Drop").performed += DropInput;
        player.FindAction("Jump").performed += JumpInput;
        player.FindAction("Jump").canceled += JumpInput;
        player.FindAction("AimUp").performed += AimUpInput;
        player.FindAction("AimUpRelease").performed += AimUpReleaseInput;
        player.Enable();
    }

    private void DropInput(InputAction.CallbackContext context)
    {
        //model.DropWeapon();
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
          
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
