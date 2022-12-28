using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    public Image hp = null;
    public GameObject gPlayer = null;
    Player player = null;

    void Start()
    {
        player = gPlayer.GetComponent<Player>();
    }

    void Update()
    {
        if (player == null)
            return;
        hp.fillAmount = player.curHp / player.maxHp;
    }
}
