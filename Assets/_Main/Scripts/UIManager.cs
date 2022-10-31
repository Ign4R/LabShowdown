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
        StatsController.OnLifeDecrese += UpdateLivesHUD;
    }

    private void UpdateLivesHUD(int player, int lives)
    {
        playersHUD[player].GetComponent<TextMeshProUGUI>().text = lives.ToString();
    }

    public void InstanceHUD(PlayerConfiguration playerConfiguration, int lives)
    {
       
        int playerIndex = playerConfiguration.PlayerIndex;
        playersHUD[playerIndex].SetActive(true);
        playersHUD[playerIndex].GetComponent<TextMeshProUGUI>().text = lives.ToString();
        playersHUD[playerIndex].GetComponentInChildren<Image>().sprite = playerConfiguration.PlayerSkin;
    }



}
