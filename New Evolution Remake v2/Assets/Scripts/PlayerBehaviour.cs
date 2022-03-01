using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public IBodyDisplay iBodyDisplay;
    public IPlayerStats iPlayerStats;
    public IDisplayData iDisplayData;

    
    private void Awake()
    {
        iBodyDisplay = GetComponent<IBodyDisplay>();
        iPlayerStats = GetComponent<IPlayerStats>();
        iDisplayData = GetComponent<IDisplayData>();

    }


    /*The initial equip, its called by the connection manager once the conection is loaded*/
    public void LoadBody(SO_DB.PlayerData db)
    {
        /*Load the Initial BodyParts from the Database*/
        if (iBodyDisplay != null) iBodyDisplay.InitialEquip(db);
        if (iPlayerStats != null) iPlayerStats.CalculateStats();
        if (iDisplayData != null) iDisplayData.DisplayStats();
    }


    // head.GetComponent<SpriteRenderer>().sprite = equip.artWork;
    public void UpdateBody(SO_DB.ItemData item)
    {
        if (iBodyDisplay != null) iBodyDisplay.UpdateBodyPart(item);
        if (iPlayerStats != null) iPlayerStats.CalculateStats();
        if (iDisplayData != null) iDisplayData.DisplayStats();
        //iPlayerStats.CalculateStats();
        //iDisplayData.DisplayStats();
    }
}