using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Instantiator : MonoBehaviour
{
    //public Button instancePlayer; 
   [SerializeField] private GameObject[] prefab;

    private string[] playerControlScheme;
    private int playerID = 0;
    private void Start()
    {
        playerControlScheme[0] = "Keyboard";
        playerControlScheme[1] = "Keyboard2";
    }
    private void Update()
    {
        if (playerID <= 1)
        {
            var _playerInput = PlayerInput.Instantiate(prefab[playerID], playerID, default);
            _playerInput.SwitchCurrentControlScheme(playerControlScheme[playerID], Keyboard.current);
            print(playerID);
            playerID++;

        }
    }
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {

    //        player.SwitchCurrentControlScheme("Keyboard", Keyboard.current);
    //    }

    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {

    //        player.SwitchCurrentControlScheme("Keyboard2", Keyboard.current);
    //    }
    //}
    //pInd = playerInput.gameObject.GetComponent<PlayerController>().playerID = playerInput.playerIndex + 1;

}
