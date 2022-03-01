using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public SO_DB db;
    public  PlayerBattleStats playerStats;
    public PlayerBattleStats enemyStats;

    public IBattleInfoDisplay iBattleInfoDisplay;
    public ITurnHandler iTurnHandler;
    ICardHanlder iCardHandler;
    // Start is called before the first frame update
    private Transform gamemanager;
    private SkillDictionary sd;


    private void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameController").transform;
        playerStats = new PlayerBattleStats();
        enemyStats = new PlayerBattleStats();
        iTurnHandler = GetComponent<ITurnHandler>();
        iBattleInfoDisplay = GetComponent<IBattleInfoDisplay>();
        sd = this.GetComponent<SkillDictionary>();
    }
    /*Calls the player so it shows its equip*/
    private void Start()
    {
        gamemanager.GetComponent<ConnectionManager>().PlayerEquip();
        PlayerInitialStats();
        /*enviar y recibir el equipo de los jugadores*/
    }


    public void PlayerTurn()
    {
        iTurnHandler.Turn(true);
        //iBattleInfoDisplay.UpdHPBar(playerStats, enemyStats);
        iTurnHandler.BattleResult(playerStats.curHP, enemyStats.curHP);

    }
    /*This will cast all the skillefects of the Skill and update the stats held by this script*/
    public void PlayCard(Skill skill)
    {
        BattleStats bs = new BattleStats();
        {
            foreach (SkillEffect effect in skill.effects)
            {
                foreach (SkillDictionary.EffectMehtod effectMethod in sd.effectDic)
                {
                    if (effect.effectID == effectMethod.id)
                    {
                        bs = effectMethod.Do(playerStats, enemyStats);
                        playerStats = bs.playerStats;
                        enemyStats = bs.enemyStats;
                    }
                }
            }
        }
    }

    //public void AnimateCardSkill(Transform caster, Transform target, Skill skill, PlayerBattleStats playerStats, PlayerBattleStats enemyStats)
    //{
    //    iTurnHandler.ExecuteSkillAnim(caster, target, skill, playerStats, enemyStats);
    //}

    public void TurnStart()
    {
        iTurnHandler.TurnStart();
    }

    public void CheckFirstTurn()
    {
        iTurnHandler.FirstTurn();
    }


    public void PlayerInitialStats()
    {
        playerStats.attack = db.playerData.attack;
        playerStats.magic = db.playerData.magic;
        playerStats.hp = db.playerData.hp;
        playerStats.curAttack = db.playerData.attack;
        playerStats.curHP = db.playerData.hp;
        playerStats.curMagic = db.playerData.magic;
        //playerStats.curtLuck
        //playerStats.Luck = db.playerData.; falta luck

    }
    public void EnemyInitialStats(SO_DB.PlayerData playerDB)
    {
        Debug.Log("receiving Items: " + playerDB.items[0].id);
        /*Por ahora solo para probar*/
        enemyStats.attack = playerDB.attack;
        enemyStats.magic = playerDB.magic;
        enemyStats.hp = playerDB.hp;
        enemyStats.curAttack = playerDB.attack;
        enemyStats.curHP = playerDB.hp;
        enemyStats.curMagic = playerDB.magic;
        /*Cambiar a futuro, debe ser a traves de la interaz y no llamar directamente al componente*/
        enemy.GetComponent<IBodyDisplay>().SetPD(playerDB);
        enemy.GetComponent<PlayerBehaviour>().LoadBody(playerDB);

    }

}
