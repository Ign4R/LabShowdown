using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerSetupMenuPrefab;
    [SerializeField] private PlayerInput input;

    private void Awake()
    {
        var rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            GameObject menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            PlayerCanvasManager playerCanvasM = menu.GetComponent<PlayerCanvasManager>();
            playerCanvasM.SetPlayerIndex(input.playerIndex);
            Button[] buttons = playerCanvasM.transform.GetChild(0).GetComponentsInChildren<Button>();
           
            for (int i = 0; i < buttons.Length; i++)
            {
                ColorBlock _colorBlock = buttons[i].colors;
                Color colorPlayer = playerCanvasM.SkinColors[input.playerIndex]; //TODO: SET SKIN COLORS
                _colorBlock.selectedColor = new Color(colorPlayer.r, colorPlayer.g, colorPlayer.b);
                buttons[i].colors = _colorBlock;
            }


        }


    }

}
