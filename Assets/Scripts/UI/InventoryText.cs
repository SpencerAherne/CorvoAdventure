using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    public Text keyCount;
    public Text scrollCount;
    public Text gemCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyCount.text = "Keys: " + Player.instance.keyCount;
        scrollCount.text = "Scrolls: " + Player.instance.arcanePulseCount;
        gemCount.text = "Gems: " + Player.instance.gemCount;
    }
}
