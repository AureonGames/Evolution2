using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    //private Transform player;
    private ConnectionManager conManager;
    private bool playerloaded;
    private IFillData iFillData;
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        conManager = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<ConnectionManager>();

        iFillData = GameObject.FindGameObjectWithTag("Loader").GetComponent<IFillData>();
    }

    private void Update()
    {
        Debug.Log("conManager.conCompleted" + conManager.conCompleted);
        Debug.Log("playerloaded" + playerloaded);
        if (conManager.conCompleted && playerloaded == false)
        {
            playerloaded = true;
            iFillData.UpdateStringData();
            conManager.PlayerEquip();

        }
    }

}
