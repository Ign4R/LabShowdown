using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    public List<PlayerConfiguration> playersList;

    [SerializeField] private int maxPlayers = 2;

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance!= null)
        {
            Debug.Log("Se trato de crear otra instancia de PlayerConfig");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
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
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            //modificar para randon minijuego
            SceneManager.LoadScene(1);
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("se unio player" + playerInput.playerIndex);
        
        if(!playerConfigs.Any(p => p.PlayerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(playerInput));
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigurations()
    {
        return playerConfigs;
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
