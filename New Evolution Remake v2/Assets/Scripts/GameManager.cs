using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public string testerName;


    //Singleton Logic This script will exist only once during the execution of the game
    void Awake()
    {
        if (Instance == null)
        {
            Application.runInBackground = true;
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    //This will get the local userID when loged with android;
    public string GetUserID()
    {
        if (Application.platform == RuntimePlatform.Android) return Social.localUser.id;
        else return testerName;
    }
    //This will get the local userName when loged with android;
    public string GetUserName()
    {
        if (Application.platform == RuntimePlatform.Android) return Social.localUser.userName;
        else return testerName;
    }
   
}
