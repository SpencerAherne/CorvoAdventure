using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenText : MonoBehaviour
{
    public Text bossKillCount;
    public Text totalGemCount;

    private void Update()
    {
        bossKillCount.text = "You've now killed the boss " + GameplayManager.instance.bossKillCount + " times!";
        totalGemCount.text = "You've collected a total of " + GameplayManager.instance.totalGemCount + " gems!";
    }
}
