using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillEffect : ScriptableObject
{
    public string effectID;
    public string effectName;
    public enum EffectType { PhisicalDamage, MagicDamage, Heal, Stun, Stats };
    public EffectType effectType;
    public string effectDescription;

    /*Eg: (30)% of the attack damage, restores (30)% healh, increase stat (20)%, increase stat(20) */
    public int effectRatio = 0;
    /*Used only for turn based attacks:  restore health eachturn for (3) turns, increase stat for (2) turns*/
    public int effectTurns = 1;

}