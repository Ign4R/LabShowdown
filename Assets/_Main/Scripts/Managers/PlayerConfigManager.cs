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
    //[Header("MIN PLAYERS TO READY GAME")]
     private int countPlayers;

    [SerializeField] private TextMeshProUGUI info;
    //[SerializeField] private Button decreaseButton;
    //[SerializeField] private Button increaseButton;
    private List<PlayerConfiguration> playerConfigs;
    public List<PlayerConfiguration> playersList;
    [SerializeField] private GameObject playerInputPrefab;
    [SerializeField] private TextMeshProUGUI minPlayersText;
    private bool canCreateSecondKeyboard = false;
    private Controls controlsInput;
    public static PlayerConfigManager Instance { get; private set; }
    public int CountPlayers { get => countPlayers; set => countPlayers = value; }

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

    public void ReadyPlayer(int index)
    {
       
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count == countPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            DontDestroyOnLoad(Instance);
            SceneManager.LoadScene(1);
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("se unio player " + (playerInput.playerIndex + 1));
        if (playerInput.playerIndex + 1 > 1) //Chequea que el minimo de players sea el index de players cuando sea mayor a 1
        {
            countPlayers = playerInput.playerIndex + 1;
            minPlayersText.text = "READY MIN";
            if(countPlayers > 3)
            {
                minPlayersText.text = "READY MAX";
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
            info.text = "Press Enter 2doKeyboard";

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
    public void SetPlayersMax()
    {
        var prefabConfig = Instantiate(playerInputPrefab, transform);
        var playerInput = prefabConfig.GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme("Keyboard2", Keyboard.current);

    }
    public void RefreshMenu()
    {
       
        SceneManager.LoadScene(0);
    }
}

public class PlayerConfiguration: MonoBehaviour
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
    private void Update()
    {
        print(PlayerIndex);
    }

}
