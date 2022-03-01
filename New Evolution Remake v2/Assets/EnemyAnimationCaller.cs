using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCaller : MonoBehaviour
{

    /*se encesita saber a quien le va a llegar el ataque, solo funciona para enemy*/
    public void EnemyAnimationCall()
    {
        BattleManager bm = GameObject.FindGameObjectWithTag("BattleManager").transform.GetComponent<BattleManager>();
        //Transform target = GameObject.FindGameObjectWithTag("Enemy").transform;
        StartCoroutine(bm.iTurnHandler.HitAnim());

    }
    public void UpdateEnemyHPBar()
    {
        BattleManager bm = GameObject.FindGameObjectWithTag("BattleManager").transform.GetComponent<BattleManager>();
        //  Transform target = GameObject.FindGameObjectWithTag("Enemy").transform;
        StartCoroutine(bm.iBattleInfoDisplay.UpdHPEnemyBar(bm.enemyStats));

    }
    public void UpdatePlayeryHPBar()
    {
        BattleManager bm = GameObject.FindGameObjectWithTag("BattleManager").transform.GetComponent<BattleManager>();
        //  Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(bm.iBattleInfoDisplay.UpdHPPlayerBar(bm.playerStats));

    }

}
