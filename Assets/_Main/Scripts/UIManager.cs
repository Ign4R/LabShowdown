using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playersHUD;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI namePlayerWin;
    [SerializeField] private Image skinWin;

    private void Awake()
    {
        DeathMatch.OnCreateHUD += InstanceHUD;
    }
    void Start()
    {
        DeathMatch.OnWinHUD += WinHUD;
        StatsController.OnLivesDecrese += UpdateLivesHUD;
        StatsController.OnUpdateHealth += UpdateHealthHUD;
        StatsController.OnDie += DieHUD;
    }
    private void Update()
    {
        
    }
    private void UpdateLivesHUD(int playerIndex, int lives)
    {    
        if (lives > 0)
        {
            playersHUD[playerIndex].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = lives.ToString();           
        }
        else
        {
            playersHUD[playerIndex].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = string.Empty;
        }
    }
    private void UpdateHealthHUD(int playerIndex, float health,float maxHealth)
    {
        playersHUD[playerIndex].GetComponentInChildren<Image>().fillAmount = health / maxHealth;
        //TODO: Parametro Lives
    }
    private void DieHUD(int playerIndex)
    {
        playersHUD[playerIndex].transform.GetChild(4).gameObject.SetActive(true);
        playersHUD[playerIndex].transform.GetChild(0).gameObject.SetActive(false);
    }
    private void WinHUD(Sprite spriteSkinWin, Color colorSkinWin, int indexWin)
    {
        skinWin.sprite = spriteSkinWin;
        skinWin.material.SetColor("_SolidOutline", new Color(colorSkinWin.r, colorSkinWin.g, colorSkinWin.b));
        namePlayerWin.text = "Player " + indexWin.ToString();
        winPanel.SetActive(true);
    }
    public void InstanceHUD(PlayerConfiguration playerConfiguration, int lives)
    {
        int playerIndex = playerConfiguration.PlayerIndex;
        playersHUD[playerIndex].SetActive(true);
        playersHUD[playerIndex].transform.GetChild(2).GetComponent<Image>().sprite = playerConfiguration.PlayerSkin;
        playersHUD[playerIndex].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = lives.ToString();
    }

    private void OnDisable()
    {
        DeathMatch.OnCreateHUD -= InstanceHUD; //TODO: IMPORTANTE DESUSCRIBIRSE WHY?
        DeathMatch.OnWinHUD -= WinHUD;
        StatsController.OnLivesDecrese -= UpdateLivesHUD;
        StatsController.OnUpdateHealth -= UpdateHealthHUD;
        StatsController.OnDie -= DieHUD;
    }


}
