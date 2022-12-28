using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEnemy_UI : MonoBehaviour
{
    public GameObject enemy = null;
    public Image hpBar = null;

    void Update()
    {
        HpGauge();
    }

    void HpGauge()
    {
        if (enemy == null)
            return;

        if (hpBar == null)
            return;

        if(enemy.GetComponent<Enemies>().curHp <= 0)
        {
            enemy.GetComponent<Enemies>().curHp = 0;
        }

        hpBar.fillAmount = enemy.GetComponent<Enemies>().curHp / enemy.GetComponent<Enemies>().maxHp;
    }
}
