
using UnityEngine;

public class PlayerStats : MonoBehaviour, IPlayerStats
{
    public SO_DB db;

    public void CalculateStats()
    {
        db.playerData.attack = 0;
        db.playerData.hp = 0;
        db.playerData.magic = 0;

        foreach (SO_DB.ItemData item in db.playerData.items)
        {
            if (item.equiped == true)
            {
                db.playerData.attack += item.equip.baseDPS;
                db.playerData.hp += item.equip.baseHP;
                db.playerData.magic += item.equip.baseMagic;
            }

        }
    }

}
