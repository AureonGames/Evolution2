using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoDisplay : MonoBehaviour
{
    public Text skillName;
    public Image skillTypeSprite;
    public string desc;
    // Start is called before the first frame update

    public void DisplaySkillInfo(Skill skill)
    {
        skillName.text = skill.SkillName;
        skillTypeSprite = skill.skillTypeIcon;
        desc = skill.desc;
    }

}
