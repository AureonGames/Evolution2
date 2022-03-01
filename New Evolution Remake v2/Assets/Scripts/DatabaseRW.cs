using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.SceneManagement;
public class DatabaseRW : MonoBehaviour
{
    public bool result = false;
    private IFillData iFillData;

    public delegate void WriteData();
   // public static event WriteData ReadyToWrite;



    private void Awake()
    {
        //Interface for filling data from database to game elements such as UI
        iFillData = GetComponent<IFillData>();
    }

    





}
