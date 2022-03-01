using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SpawnPlayerCommand : IGameCommand
{
    /*Send and recieve stats from the other player*/


    string playerPrefabName;
    BattleManager battleManager;
    /*the Enemy Battle Stat*/
    public TurnInfo turnInfo;
    /*The Enemy DB*/
    public BattlePlayerData db;

    public SpawnPlayerCommand()
    {
        this.playerPrefabName = "";
        battleManager = null;
    }

    /*Set the position of the player from the server*/
    public SpawnPlayerCommand(string playerPrefabName, TurnInfo turnI)
    {
        this.playerPrefabName = playerPrefabName;
        turnInfo = turnI;
        turnInfo.bs.enemyStats = turnI.bs.enemyStats;
    }

    public SpawnPlayerCommand(string playerPrefabName, BattlePlayerData dbData)
    {
        this.playerPrefabName = playerPrefabName;
        db = dbData;
    }


    public void execute()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        /*update the Stats poner aqui las animaciones para que el cambio de stats no sea tan brusco*/
        /*Primero se ejecutan las animaciones de turno*/
        battleManager.playerStats = turnInfo.bs.enemyStats;
        battleManager.enemyStats = turnInfo.bs.playerStats;
        battleManager.PlayerTurn();
        /*Play Turn Animations*/
        /*Transform caster, Transform tarjet, Skill skill, PlayerBattleStats playerStats, PlayerBattleStats enemyStats*/
        /*skillID Comes from the database*/
        Skill skill = GetSkillID(turnInfo.skillID);
        battleManager.iTurnHandler.ExecuteSkillAnim(battleManager.enemy, battleManager.player, skill, battleManager.playerStats, battleManager.enemyStats);


    }
    public Skill GetSkillID(string skillID)
    {
        SkillList sl = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<SkillDictionary>().skillList;
        
        foreach (Skill skill in sl.skills)
        {
            if (skill.skillID == skillID)
            {
                return skill;
            }
        }
        return null;
    }


    public string GetTurnData()
    {
        // battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        //bs.playerStats = battleManager.playerStats;
        //bs.enemyStats = battleManager.enemyStats;
        Debug.Log(turnInfo.bs.playerStats.attack);
        /*No esta funcionando*/
        string battleDataString = JsonUtility.ToJson(turnInfo);
        Debug.Log("Turn data: " + battleDataString);
        return battleDataString;
    }


    public string GetPlayerInfo(BattlePlayerData db)
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

        string battleDataString = JsonUtility.ToJson(db);

        return battleDataString;
    }


    public string getPrefabName()
    {
        return playerPrefabName;
    }



    /*Called when i received the data from the enemy database*/
    public void FirstLoad(bool isServer)
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

        /*Instantiate the player*/
        Debug.Log("receiving Items: " + db.playerData.items[0].id);

        battleManager.GetComponent<BattleManager>().EnemyInitialStats(db.playerData);


        /*check who starts*/
        if (!isServer) /*the server already knows*/
        {
            /*Malo, corregir*/
            if (db.firstTurn == true) /*true means the enemy starts*/
            {
                battleManager.iTurnHandler.PlayerTurn = false;
                battleManager.iTurnHandler.ShowEnemyTurnMessage();
            }
            else
            {
                battleManager.iTurnHandler.PlayerTurn = true;
                battleManager.TurnStart();
            }
        }
    }



    /*Send Message*/
    public NetworkMessage asNetworkMessageFrom(string sender, string type)
    {
        if (type == "turn")
        {
            return new NetworkMessage(
                sender, GameOpCode.TURN.ToString(), playerPrefabName, GetTurnData(), null);
        }
        else if (type == "playerData")
        {
            return new NetworkMessage(
            sender, GameOpCode.ENEMY_DATA.ToString(), playerPrefabName, GetPlayerInfo(db), null);


        }
        else return null;
    }


    /*process received message*/
    public static SpawnPlayerCommand fromNetworkMessage(NetworkMessage msg, string type)
    {
        if (type == "turn")
        {
            /*Player prefab name, vector 3 position*/
            PlayerBattleStats pbs = new PlayerBattleStats();
            return new SpawnPlayerCommand(msg.Data1, Utils.StringToBattleStats(msg.Data2));

        }
        else if (type == "playerData")
        {
            return new SpawnPlayerCommand(msg.Data1, Utils.StringToBattleStats(msg.Data2, type));
        }
        else return null;
    }

}
