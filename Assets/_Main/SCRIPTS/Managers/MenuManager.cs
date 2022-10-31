using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("MINIM PLAYERS TO READY GAME")]
    [SerializeField] private int countPlayers;

    [SerializeField] private TextMeshProUGUI info;
    //[SerializeField] private Button decreaseButton;
    //[SerializeField] private Button increaseButton;
    private List<PlayerConfiguration> playerConfigs;
    public List<PlayerConfiguration> PlayersList { get; private set; }
    [SerializeField] private GameObject playerInputPrefab;
    [SerializeField] private TextMeshProUGUI minPlayersText;
    private bool canCreateSecondKeyboard = false;
    private Controls controlsInput;
    public static MenuManager Instance { get; private set; }
    public int CountPlayers { get => countPlayers; set => countPlayers = value; }

    private void Awake()
    {

        controlsInput = new Controls();
        if (Instance != null)
        {
            Debug.Log("Se trato de crear otra instancia de PlayerConfig");
        }
        else
        {
            Instance = this;
            playerConfigs = new List<PlayerConfiguration>();
            PlayersList = new List<PlayerConfiguration>();
        }
    }
    private void Start()
    {
        minPlayersText.text = "NO READY";
    }

    private void OnEnable()
    {
        controlsInput.Enable();
    }

    private void Update()
    {
        if (canCreateSecondKeyboard) CreateSecondKeyboard();
    }
    public void SetPlayerSkin(int index, Sprite skin)
    {      
        playerConfigs[index].PlayerSkin = skin;
       
    }

    public void SetColorPlayer(int index, Color skinColor)
    {
        playerConfigs[index].SkinColor = skinColor;
    }

    public void ReadyPlayer(int index)
    {

        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count > 1 && playerConfigs.All(p => p.IsReady == true))
        {
            DontDestroyOnLoad(Instance);
            SceneManager.LoadScene(1);
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("se unio player " + (playerInput.playerIndex + 1));

        countPlayers = playerInput.playerIndex + 1;
        if (countPlayers > 1) //Chequea que el minimo de players sea el index de players cuando sea mayor a 1
        {
            minPlayersText.text = "READY MIN"; //TODO:
            if (countPlayers > 3)
            {
                minPlayersText.text = "READY MAX"; //TODO:
            }
        }
        if (!playerConfigs.Any(p => p.PlayerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(playerInput));

        }
        if (playerInput.currentControlScheme == "Keyboard")
        {
            canCreateSecondKeyboard = true;
            info.text = "Press Enter 2doKeyboard"; //TODO:

        }
    }

    public List<PlayerConfiguration> GetPlayerConfigurations()
    {
        return playerConfigs;
    }
    public void CreateSecondKeyboard()
    {
        bool temp = controlsInput.Player.AnyKey.ReadValue<float>() > 0.1f;
        if (temp) //TECLA PARA INSTANCIA EL SEGUNDO PLAYER KEYBOARD
        {
            var prefabConfig = Instantiate(playerInputPrefab, transform);
            var playerInput = prefabConfig.GetComponent<PlayerInput>();
            playerInput.SwitchCurrentControlScheme("Keyboard2", Keyboard.current);
            canCreateSecondKeyboard = false;
            info.text = "Connect a Joystick";
        }
    }
    public void RefreshMenu()
    {
        SceneManager.LoadScene(0);
    }
}

public class PlayerConfiguration 
{
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Sprite PlayerSkin { get; set; }
    public Color SkinColor { get; set; }

    public PlayerConfiguration(PlayerInput playerInput)
    {
        PlayerIndex = playerInput.playerIndex;
        Input = playerInput;
    }

}
