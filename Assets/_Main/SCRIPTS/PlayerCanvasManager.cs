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

    public Color[] SkinColors { get => skinColors; private set => skinColors = value; }

    private void Start()
    {
    }
    void Update()
    {

        if (Time.time > ignoreInputTime)
        {
            inputEnable = true;
        }
    }

    public void PlayAudio()
    {
        AudioManager.Instance.Play("menusoundselect");
    }

    public void SetPlayerIndex(int playerInput)
    {
      
        playerIndex = playerInput;
        titleText.SetText("Player " + (playerInput + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetSkin(Sprite skin)
    {
        if (!inputEnable) return;

        MainMenuManager.Instance.SetPlayerSkin(playerIndex, skin);
        MainMenuManager.Instance.SetColorPlayer(playerIndex, SkinColors[playerIndex]);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void SetAnim(RuntimeAnimatorController animRuntime)
    {
        if (!inputEnable) return;
        MainMenuManager.Instance.SetAnim(playerIndex, animRuntime);

    }

    public void ReadyPlayer()
    {
        if (!inputEnable) return;

        MainMenuManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
        readyInfo.gameObject.SetActive(true);
    }
}
