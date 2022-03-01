using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
[Serializable]
public class Equip : ScriptableObject
{
    public string itemId;
    public string itemName;
    public int baseDPS;
    public int baseHP;
    public int baseMagic;
    public int manaCost;
    public string bodyType;
    public Sprite artWork;
    public Transform bodyPart;
    // public Transform equip;
    public List<Skill> skills;
}
