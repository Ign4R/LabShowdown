using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playersHUD;

    private void Awake()
    {
        DeathMatch.OnCreateHUD += InstanceHUD;
    }
    void Start()
    {     
        StatsController.OnLivesDecrese += UpdateLivesHUD;
        StatsController.OnUpdateHealth += UpdateHealthHUD;
    }
    private void Update()
    {
        
    }
    private void UpdateLivesHUD(int playerIndex, int lives)
    {
        playersHUD[playerIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = lives.ToString();
    }
    private void UpdateHealthHUD(int playerIndex, float health,float maxHealth)
    {
        playersHUD[playerIndex].GetComponentInChildren<Image>().fillAmount = health / maxHealth;     
        //TODO: Parametro Lives
    }

    public void InstanceHUD(PlayerConfiguration playerConfiguration, int lives)
    {
       
        int playerIndex = playerConfiguration.PlayerIndex;
        playersHUD[playerIndex].SetActive(true);
        playersHUD[playerIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = lives.ToString();
        playersHUD[playerIndex].transform.GetChild(1).GetComponentInChildren<Image>().sprite = playerConfiguration.PlayerSkin;
    }



}
