using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RoundEnd : MonoBehaviour
{
    public void ReStart()
    {
        GameManager.instance.isReStarted = true;
        GameManager.instance.isRoundEnd = false;
        GameManager.instance.NotifyCharacters();
    }
}
