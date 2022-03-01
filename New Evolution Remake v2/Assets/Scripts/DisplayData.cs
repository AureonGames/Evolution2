using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour, IDisplayData
{
    public SO_DB db;
    public Text dsp;
    public Text hp;
    public Text ap;
    public Text luck;

    public void DisplayStats()
    {
        if (dsp) dsp.text = db.playerData.attack.ToString();
        if (hp) hp.text = db.playerData.hp.ToString();
        if (ap) ap.text = db.playerData.magic.ToString();
       //luck.text = db.playerData.luck.ToString();
    }



}
