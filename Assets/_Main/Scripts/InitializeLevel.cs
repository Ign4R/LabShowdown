using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private TextMeshProUGUI[] playersLives;
    [SerializeField] private int playersLivesQuantity;

    
    void Start()
    {
        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigurations().ToArray();

        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerController>().InitializePlayer(playerConfigs[i]);
            player.GetComponent<StatsController>().SetLifes(playersLivesQuantity);
            playersLives[i].text = playersLivesQuantity.ToString();
        }
    }

    
  
}
