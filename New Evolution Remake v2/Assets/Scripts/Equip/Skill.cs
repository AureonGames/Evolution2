using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public string skillID;
    public string SkillName;
    public Sprite artImage;
    public string desc;
    public string skillType; /*Physical Attack (PA), MagicAttack (MA), Support (SUP)*/
    public Image skillTypeIcon;

    [SerializeField]
    public List<string> animationType; /*0 = Head, 1 = Chest, 2 = Arms, 3 = Legs*/

    [SerializeField]
    public List<SkillEffect> effects; /*This is used by the Effect Dictionary*/
}