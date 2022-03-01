//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CardInteraction : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    //PlayerSpawner ps;

    BattleManager bm;
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

    }
    private void Awake()
    {
       // ps = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<PlayerSpawner>();
        bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
    }


    void Update()
    {/*si es mi turno*/
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;
            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            RaycastResult result = results[0];
            //foreach (RaycastResult result in results)
            //{
            if (result.gameObject.CompareTag("Card"))
                {
                /*This will go to the Card Transform and will get the skill of the card*/
                    Skill skill = result.gameObject.GetComponent<CardSkillDisplay>().skill;
                    bm.PlayCard(skill);

                TurnInfo turnInfo = new TurnInfo();
                turnInfo.bs = new BattleStats();

                turnInfo.bs.playerStats = bm.playerStats;
                turnInfo.bs.enemyStats = bm.enemyStats;
                turnInfo.skillID = skill.skillID;
                /*You cannot longer interact with the cards*/
                bm.iTurnHandler.DeleteCurrentCards();

                /*Cast the animation using the player and enemy prefabs, the stats to update the data at the right moment and the skill to know which animations to play*/
                bm.iTurnHandler.ExecuteSkillAnim(bm.player, bm.enemy, skill, bm.playerStats, bm.enemyStats);
               // ps.SendPlayInfo(turnInfo); /*debo enviar los datos de la jugada (crear nueva estructura)*/
               // bm.iTurnHandler.EndTurn();
                }
            else
                {
                    Debug.Log("I Missed the Card");
                }

            //}
        }
    }
}