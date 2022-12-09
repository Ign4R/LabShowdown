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

    private Animator anim;

    public PlayerConfiguration PlayerConfig { get; private set; }

    private InputAction attack;

    private void Awake()
    {
        skin = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if(canTest) Test();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        PlayerConfig = pc;
        skin.sprite = pc.PlayerSkin;
        anim.runtimeAnimatorController = pc.AnimRuntime;
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
        model.Raycasts();
        model.Timer();
        model.Movement(movement.ReadValue<Vector2>().x);
        model.Attack(attack.ReadValue<float>());
        model.Jump(movement.ReadValue<Vector2>().x);
        model.VariableJump();
        model.FallingSpeedIncrease();
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
        model.DropWeapon();
    }
    private void JumpInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {

            model.AlreadyJumped = false;
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
