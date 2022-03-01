using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public EquipList equipList;
    public SO_DB db;

    public Image artworkImage;
    public Transform starsPanel;
    public Transform starGO;

    public Text manaCost;
    public Text cardName;
    public string bodyType;
    private SO_DB.ItemData itemSO;



    /*This will load each card, it will be called for each card by FillData Script*/
    public void Display(SO_DB.ItemData item)
    {
        /*Load the stars of the card*/
        for (int i = 1; i <= int.Parse(item.stars); i++)
        {
        Instantiate(starGO, starsPanel);
        if (i > 5) break;
        }
        Equip equip = item.equip;
        itemSO = item;
        artworkImage.sprite = equip.artWork;
        cardName.text = equip.itemName;
        manaCost.text = equip.manaCost.ToString();
        bodyType = equip.bodyType;
    }

    public void ShowEquip()
    {
        GameObject panel = GameObject.FindGameObjectWithTag("EquipInfo");

        Debug.Log(panel.name);
        
        panel.GetComponent<EquipInfo>().ShowObjectInfo(itemSO);
    }
}
