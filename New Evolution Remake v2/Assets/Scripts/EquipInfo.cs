using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInfo : MonoBehaviour
{
    public Text equipName;
    public Text dsp;
    public Text hp;
    public Text ap;
    public Text luck;
    public Image itemArtwork;
    public Transform player;
    private SO_DB.ItemData selectedItem;
    private ISkillDisplay iSkillDisplay;
    public SO_DB db;

    private void Awake()
    {
        iSkillDisplay = GetComponent<ISkillDisplay>();
    }

    public void ShowObjectInfo(SO_DB.ItemData item)
    {
        Equip equipSO = item.equip;

        itemArtwork.sprite = equipSO.artWork;
        equipName.text = equipSO.itemName;
        dsp.text = equipSO.baseDPS.ToString();
        hp.text = equipSO.baseHP.ToString();
        ap.text = equipSO.baseMagic.ToString();
        luck.text = "0";
        GetComponent<Animator>().SetBool("show", true);
        selectedItem = item;
        iSkillDisplay.GetSkills(equipSO);
    }
    public void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Equip") || hit.collider.gameObject.CompareTag("EquipInfo"))
                {
                    GetComponent<Animator>().SetBool("show", true);
                }
            }
        }
    }
    /*updates the equip when the player touches the bodypart icon (button)*/
    public void UpdatePlayerEquip()
    {

        player.GetComponent<PlayerBehaviour>().UpdateBody(selectedItem);
        UpdateDBSkills();
    }


    public void UpdateDBSkills()
    {
        db.playerData.skills.Clear();
        Debug.Log("cantidad de skills: " + db.playerData.skills.Count);
        foreach (SO_DB.ItemData item in db.playerData.items)
        {
            if (item.equiped == true)
            {
                foreach (Skill skill in item.equip.skills)
                {
                    db.playerData.skills.Add(skill);
                }
            }

        }
    }


}
