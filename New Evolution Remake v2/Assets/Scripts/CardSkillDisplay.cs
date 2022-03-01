using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSkillDisplay : MonoBehaviour
{
    public Skill skill;
    public Text cardName;
    public Text cardDesc;
    public Image splashArt;
    // Start is called before the first frame update
    void Start()
    {
        cardDesc.text = skill.desc;
        cardName.text = skill.SkillName;
         splashArt.sprite = skill.artImage;

    }
}
