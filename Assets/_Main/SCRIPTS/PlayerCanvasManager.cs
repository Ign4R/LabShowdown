using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasManager : MonoBehaviour
{
   
    private int playerIndex;
    [SerializeField] private Color[] skinColors;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject readyInfo;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button readyButton;

    private float ignoreInputTime = 0.1f;
    private bool inputEnable;
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnable = true;
        }
    }
    public void SetPlayerIndex(int playerInput)
    {
        playerIndex = playerInput;
        titleText.SetText("Player" + (playerInput + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetSkin(Sprite skin)
    {
        if (!inputEnable) return;

        MenuManager.Instance.SetPlayerSkin(playerIndex, skin);
        MenuManager.Instance.SetColorPlayer(playerIndex, skinColors[playerIndex]);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnable) return;

        MenuManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
        readyInfo.gameObject.SetActive(true);
    }
}
