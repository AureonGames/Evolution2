using System.Collections;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections.Generic;
using System;

public class ConnectionManager : MonoBehaviour
{
    public SO_DB db;
    public EquipList equipList;

    public bool result = false;
    private bool authenticated = false;
    private bool connectedDB = false;
    public bool conCompleted = false;
    private string userID = "hydepambito3";
    private bool connected;
    public bool goToNextStep = false;
    private IFillData iFillData;
    private List<SO_DB.ItemData> items;
    private SO_DB.PlayerData playerData;
    private Transform player;

    private void Awake()
    {
        //Interface for filling data from database to game elements such as UI
        iFillData = GameObject.FindGameObjectWithTag("Loader").GetComponent<IFillData>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        BeginConnection();
    }

    public void BeginConnection()
    {
        // Initialize Play Games Configuration and Activate it.
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false /*forceRefresh*/).Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        //output.text = ("SignInOnClick: Play Games Configuration initialized");

        //Connect to Auth
        authenticated = Authentication();
        if (true)//(authenticated)
        {
            StartCoroutine(ConnectionProcess());
        }

        items = new List<SO_DB.ItemData>();
        playerData = new SO_DB.PlayerData();
    }


    /*Calls all the other rutines*/
    IEnumerator ConnectionProcess()
    {
        PlayFabLogin pf = new PlayFabLogin(); 
        StartCoroutine(Login()); /*Starts the GooglePlayGames Login Porcess*/
        while (!goToNextStep)
        {
            yield return null;
        }
        goToNextStep = false;

        //Here I should Start the Playfab Authentication Rutine

        StartCoroutine(pf.PlayfabAuth(this));
        while (!goToNextStep)
        {
            yield return null;
        }
        goToNextStep = false;

        StartCoroutine(RetrievePlayerData()); /*Reads from the Firebase Database*/
        while (!goToNextStep)
        {
            yield return null;
        }
        goToNextStep = false;

        StartCoroutine(FillDB()); /*Writes from the Firebase Database into the DB Scriptable object*/
        while (!goToNextStep)
        {
            yield return null;
        }
        goToNextStep = false;


        if(iFillData != null) iFillData.UpdateStringData(); /*Writes from the DB scriptable object into the Place Holders*/

        yield return null;

        conCompleted = true;

        //PlayerEquip();

    }

    private IEnumerator FillDB()
    {
        db.playerData = playerData;
        db.playerData.items.Clear();
        db.playerData.items = items;
        /*Attach each item with the corresponding Equip Scriptable Object prefab*/
        foreach (SO_DB.ItemData item in items)
        {
            foreach (Equip equip in equipList.equipList)
            {
                if (item.itemID == equip.itemId)
                {
                    item.equip = equip;
                }
            }
        }
        goToNextStep = true;
        yield return null;
    }

    /*Calls the auth Process*/
    public bool Authentication()
    {
        /*GooglePlayAuth*/
        Social.Active.Authenticate(Social.localUser, (bool success) =>
        {
            if (success)
            {
                result = true;
            }
            else
            {
                result = false;
                /*For now we will try to connect to database even if googleplay fails*/
                // dbm.ConnectToDatabase();
            }
        });
        return result;
    }


    /*Connects to de Firebase database*/
    IEnumerator Login()
    {
        string username;
        if (Application.platform == RuntimePlatform.Android) username = db.userData.userName;
        else username = userID;
        username = username + "@evolution.com";
        string password = "ANonSecurePassword!ñ";

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(username, password).ContinueWith((obj) =>
        {
            if (obj.IsFaulted) //Not yet registered.
            {
                FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance).CreateUserWithEmailAndPasswordAsync(username, password).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        connected = true;
                        goToNextStep = true;
                    }
                    if (task.IsFaulted)
                    {
                        connected = false;
                        // SceneManager.LoadScene(0); //Recargar la escena y volver a intentar
                        Debug.Log("Error al conectar: " + username);
                        goToNextStep = true;
                    }
                });
            }
            else
            {
                Debug.Log("Bienvenido: " + username);
                connected = true;
                goToNextStep = true;
            }

        });
        yield return null;
    }

    /*Retrieves the player data from the database*/
    public IEnumerator RetrievePlayerData()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("Players").Child(db.userData.userName).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                if (task.Result.Exists) //Player Already Exists
                {
                    string jsonPlayer = task.Result.Child("Data").GetRawJsonValue();

                    IEnumerable children = task.Result.Child("Data").Child("Items").Children;

                    playerData = JsonUtility.FromJson<SO_DB.PlayerData>(jsonPlayer);

                    foreach (DataSnapshot t in children)
                    {
                        /*Create a new list member for an item*/
                        SO_DB.ItemData newItem = new SO_DB.ItemData();
                        string itemInfo = t.GetRawJsonValue();
                        newItem = JsonUtility.FromJson<SO_DB.ItemData>(itemInfo);
                        newItem.id = t.Key;
                        items.Add(newItem);
                        
                    }

                    //Start Delegated Function
                    goToNextStep = true;
                    

                }
                else
                {
                    //start_menu.gameObject.SetActive(false);
                    //transform.Find("Loading").gameObject.SetActive(false);
                    //camMovementEnabled = false;
                    //SceneManager.LoadScene(1); //Ir a Huevo
                    goToNextStep = true;
                }
            }
            else
            {
                goToNextStep = true;
                SceneManager.LoadScene(0);
            }
        });
        yield return null;

    }
    /*Once the data is retrieved and ready you can load the player equip*/
    public void PlayerEquip()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform; /*i lose the reference when change scene*/
        Debug.Log("the player: " + player.name);
        player.GetComponent<BodyDisplay>().pd = player.GetComponent<BodyDisplay>().db.playerData;

        Debug.Log("the player bodyparts holder (hp): " + player.GetComponent<BodyDisplay>().pd.hp);
        Debug.Log("the player: " + player.name);
        Debug.Log("No encuentro al Player");
        player.GetComponent<PlayerBehaviour>().LoadBody(db.playerData);
    }







}