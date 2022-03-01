using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Utils
{
    /**
     * https://forum.unity.com/threads/vector3-from-string-solved.46223/#post-3083644
     */
    static public TurnInfo StringToBattleStats(string s)
    {
        TurnInfo bs = new TurnInfo();
        bs = JsonUtility.FromJson<TurnInfo>(s);
        Debug.Log(s);
        Debug.Log("Asi se recibe el TurnIfno: " + bs.bs.playerStats.attack);
        return bs;

    }
    static public BattlePlayerData StringToBattleStats(string s, string aux /*no se usa pero es para diferenciar metodos (borrar en futuro*/)
    {
        BattlePlayerData bdInstance = new BattlePlayerData();
        bdInstance.playerData = new SO_DB.PlayerData();
        bdInstance = JsonUtility.FromJson<BattlePlayerData>(s);
        Debug.Log("Asi se recibe JSON: " + s);
        Debug.Log("Asi se recibe: " + bdInstance.playerData.win);
        return bdInstance;

    }

}
