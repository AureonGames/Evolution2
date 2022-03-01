using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillList : ScriptableObject
{
    [SerializeField]
    public List<Skill> skills;
}
