using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*This script will write from the DB to the placeHolders*/

public class FillData : MonoBehaviour, IFillData
{
    [Serializable]
    public class PlayerDataHolder
    {
        [SerializeField] public Text Win;
        [SerializeField] public Text Lose;
        [SerializeField] public Text Level;
        [SerializeField] public Text playerName;
    }

    [Serializable]
    public class CurrencyHolder
    {
        [SerializeField] public Text dna;
        [SerializeField] public Text gems;
    }
    public CurrencyHolder curr;
    public PlayerDataHolder play;
    public Transform bodyPartInventoryPanel;
    public Transform bodyPartItemPref;
    public Image ProfileImage;
    /*List of cards, ir order to hide or show when you select different filters*/
    private List<Transform> ItemList;
    public SO_DB db;



    public void UpdateStringData()
    {
        /*The logic is the following:
         - Write the player info into the placeholders lvl, wins, losses etc.
         - Update the currency of the player
         - Instantiate the bodypart Cards and call their "fill card info" process
         - Display only heads (this procedure will work for any bodytype button)
         */
        UpdatePlayerData(db);
        UpdateCurrency(db);
        ClearList();
        UpdateInventory(db, bodyPartItemPref);
        DisplayBodyType("Head"); /*default filer when you go to the edit menu*/
    }

/*This will fill the player data in the dataholders*/
public void UpdatePlayerData(SO_DB db)
{
    play.playerName.text = db.userData.userName.ToString();
    play.Win.text = db.playerData.win.ToString();
    play.Lose.text = db.playerData.lose.ToString();
}

/*This will fill the player currency in the data holders*/
public void UpdateCurrency(SO_DB db)
{
    curr.dna.text = db.playerData.money.ToString();
    curr.gems.text = db.playerData.diamonds.ToString();
}

/*This will clear the panel*/
public void ClearList()
{
    foreach (Transform child in bodyPartInventoryPanel)
    {
        Destroy(child.gameObject);
    }
}

/*This will fill the items in the database object*/
public void UpdateInventory(SO_DB db, Transform cardPref)
{
        ItemList = new List<Transform>();
    foreach (SO_DB.ItemData item in db.playerData.items)
    {
        Transform newCard = Instantiate(cardPref, bodyPartInventoryPanel);
        ItemList.Add(newCard);
        newCard.GetComponent<CardDisplay>().Display(item);

    }

}

    /*Show the bodycards items in the inventory applying the filters*/
public void DisplayBodyType(string newBodyType)
{
    foreach (Transform item in ItemList)
    {
        if (newBodyType == item.GetComponent<CardDisplay>().bodyType)
        {
            item.gameObject.SetActive(true);
        }
        else
        {
            item.gameObject.SetActive(false);
        }
    }
}



}