using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerConfigManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI info;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    private List<PlayerConfiguration> playerConfigs;
    public List<PlayerConfiguration> playersList;

    [SerializeField] private GameObject playerInputPrefab;
    [SerializeField] private int maxPlayers = 2;
    [SerializeField] private TextMeshProUGUI maxPlayersText;
    private bool canCreateSecondKeyboard=false;
    private Controls controlsInput;
    public static PlayerConfigManager Instance { get; private set; }

    private void Start()
    {
       
        //maxPlayers = int.Parse(maxPlayersText.text);
    }

    private void Awake()
    {
        controlsInput = new Controls();
        if (Instance!= null)
        {
            Debug.Log("Se trato de crear otra instancia de PlayerConfig");
        }
        else 
        {
            Instance = this;           
            playerConfigs = new List<PlayerConfiguration>();
            playersList = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerSkin(int index, Sprite skin)
    {
        playerConfigs[index].PlayerSkin = skin;
    }

    public void ReadyPlayer(int index)
    {
        decreaseButton.interactable = false;
        increaseButton.interactable = false;
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            DontDestroyOnLoad(Instance);
            SceneManager.LoadScene(1);
        }
    }
    private void OnEnable()
    {
        controlsInput.Enable();
    }
    private void OnDisable()
    {
        controlsInput.Disable();
    }
    private void Update()
    {
      
        print(controlsInput);
        if (canCreateSecondKeyboard) CreateSecondKeyboard();
        print(maxPlayers);
    }
    public void HandlePlayerJoin(PlayerInput playerInput)
    {
      
        Debug.Log("se unio player " + playerInput.playerIndex + 1);
        
        if(!playerConfigs.Any(p => p.PlayerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(playerInput));

        }
        if (playerInput.currentControlScheme == "Keyboard")
        {
            info.text = "Press Enter 2doKeyboard";
            canCreateSecondKeyboard = true;
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
    public void SetPlayersMax(int num)
    {
        var countPlayer = maxPlayers += num;
        maxPlayersText.text = countPlayer.ToString();

        if (countPlayer == 1) decreaseButton.interactable = false;
        else decreaseButton.interactable = true;
        if (countPlayer == 4) increaseButton.interactable = false;
        else increaseButton.interactable = true;      
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

    public PlayerConfiguration(PlayerInput playerInput)
    {
        PlayerIndex = playerInput.playerIndex;
        Input = playerInput;
    }
}
