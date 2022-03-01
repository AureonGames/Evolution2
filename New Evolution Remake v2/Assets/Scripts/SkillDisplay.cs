using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDisplay : MonoBehaviour, ISkillDisplay
{
    public Transform equipSkillPanel;
    public Transform skillPref;
    public SO_DB db;

    public void GetSkills(Equip equip)
    {
        ClearSkillList();
        /*Clear the skill list on the SO database*/
        foreach (Skill skill in equip.skills)
        {
            Transform newSkill = Instantiate(skillPref, equipSkillPanel);
            newSkill.GetComponent<SkillInfoDisplay>().DisplaySkillInfo(skill);
        }


    }
    public void ClearSkillList()
    {
        foreach (Transform skill in equipSkillPanel)
        {
            Destroy(skill.gameObject);
        }

    }
}
