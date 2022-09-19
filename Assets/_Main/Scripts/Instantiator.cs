using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Instantiator : MonoBehaviour
{
    //*public Button instancePlayer*//
    //TODO: Probar con un boton la instancia del player
    [SerializeField] private GameObject[] prefabSkins;
    private InputDevice[] device= new InputDevice[4];
    private string[] playerControlScheme= new string[4];
 
    [Range(1, 4)]
    [SerializeField] private int maxPlayersInScene;
    private int numberOfPlayers = 0;
    [SerializeField] public Transform[] spawns;
    [SerializeField] private Transform spawnTest;
    private void Start()
    {
        ///Devices
        Gamepad currentGamepad = Gamepad.current;
        Keyboard currentKeyboard = Keyboard.current;
        device[0] = currentKeyboard;
        device[1] = currentKeyboard;
        device[2] = currentGamepad;
        device[3] = currentGamepad;

        ///ControlScheme
        playerControlScheme[0] = "Keyboard";
        playerControlScheme[1] = "Keyboard2";
        playerControlScheme[2] = "Controller";


    }
    private void Update()
    {
        if (numberOfPlayers < maxPlayersInScene) //TODO: PROBAR DE MANDAR ESTO AL START
        {
            var  temp = PlayerInput.Instantiate(prefabSkins[numberOfPlayers], numberOfPlayers, playerControlScheme[numberOfPlayers], default, device[numberOfPlayers]);
            temp.transform.position = spawns[numberOfPlayers].position;
            temp.transform.rotation = spawns[numberOfPlayers].rotation;
            print(numberOfPlayers+1);
            numberOfPlayers++;
        }
       

    }


}
