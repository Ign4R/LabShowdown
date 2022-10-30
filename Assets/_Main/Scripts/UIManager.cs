using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] playersLives;

    void Start()
    {
        StatsController.OnLifeDecrese += LivesTextUpdate;
    }

    private void LivesTextUpdate(int lives)
    {
        var temp = PlayerConfigManager.Instance.CountPlayers;
        //playersLives[player].text = lives.ToString();
        playersLives[temp].text = lives.ToString();
    }


}
