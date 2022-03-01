using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDictionary : MonoBehaviour
{

    public  BattleManager bm;
    public SkillList skillList;
    public List<EffectMehtod> effectDic;


    /*
     * in order to perform the skills each card will have reference to different effects in a list
     * this methods needs to know:
     *  - the target to know if the skill will affect the caster or the enemy
     *  - the stats, in order to calculate the total effect power
     *  - the skill in order to apply the effect
         
         */
    //public int HeadButt(Transform target, string type)
    //{
    //    /*Hace 20% del daño total como daño de ataque*/
    //}

    private void Awake()
    {
        bm = this.GetComponent<BattleManager>();
        effectDic = new List<EffectMehtod>();
    }

    private void Start()
    {
        /*Here we will list all the functions with its respective id, not sure if its the best way to do it*/

        /*PhysicalDamage*/
        PhysicalDamage pd = new PhysicalDamage();
        pd.id = "3"; /*No se como setear este valor desde la definicion de la clase (override int??)*/
        effectDic.Add(pd);
        /******************/
        Heal heal = new Heal();
        heal.id = "0";
        effectDic.Add(heal);


        foreach (EffectMehtod skill in effectDic)
        {
            Debug.Log(skill.id);
        }
        /*Heal*/



    }

    public abstract class EffectMehtod
    {
        public string id;
        public abstract BattleStats Do(/*int effectRatio, int effectTurn, */PlayerBattleStats playerStats, PlayerBattleStats enemyStats);
        public BattleStats UpdateBS (PlayerBattleStats playerStats, PlayerBattleStats enemyStats)
        {
            BattleStats bs = new BattleStats();
            bs.enemyStats = enemyStats;
            bs.playerStats = playerStats;
            return bs;
        }
    }

    /*The method of each skill*/

    public class PhysicalDamage : EffectMehtod
    {
        public override BattleStats Do (/*int effectRatio, int effectTurn,*/ PlayerBattleStats playerStats, PlayerBattleStats enemyStats)
        {

            int ad = (int)(playerStats.curAttack/3); //e.g: 30/100 = 0.30 = 30%
            enemyStats.curHP -= ad;
            return UpdateBS(playerStats, enemyStats);
        }
    }


    public class Heal : EffectMehtod
    {
        public override BattleStats Do(/*int effectRatio, int effectTurn,*/ PlayerBattleStats playerStats, PlayerBattleStats enemyStats)
        {

            int heal = (int)(playerStats.curMagic / 4); //1/4 of magic
            playerStats.curHP += heal;
            return UpdateBS(playerStats, enemyStats);

        }
    }


}
