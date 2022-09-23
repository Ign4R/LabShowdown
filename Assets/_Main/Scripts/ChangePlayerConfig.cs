using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerConfig : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    [SerializeField] private SpriteRenderer skin;

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        skin.sprite = pc.PlayerSkin;
    }
}
