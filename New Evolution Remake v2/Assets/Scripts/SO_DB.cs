using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SO_DB : ScriptableObject
{
    public PlayerData playerData;
    public UserData userData;

    [Serializable]
    public class PlayerData
    {
        public int win;
        public int lose;
        public int lvl;
        public int chest;
        public int crown;
        public int diamonds;
        public int money;
        public int attack;
        public int hp;
        public int magic;
        public bool turn = false;
        public List<ItemData> items;
        public List<Skill> skills;

    }
    [Serializable]
    public class UserData
    {
        public string userName;
        public string password;
    }

    [Serializable]
    public class ItemData
    {
        public string id;
        public string rarity;
        public string stars;
        public bool equiped;
        public string itemID;
        public Equip equip;
    }
}