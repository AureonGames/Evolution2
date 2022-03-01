using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoDisplay : MonoBehaviour, IBattleInfoDisplay
{
    public Text playerName;
    public Image playerFace;
    public Transform playerHPBar;
    /*Enemy UI*/
    public Text enemyName;
    public Image enemyFace;
    public Transform enemyHPBar;




    public IEnumerator UpdHPPlayerBar(PlayerBattleStats playerBS)
    {
        //Debug.Log("Current HP: "+ enemyStats.curHP);
        //Debug.Log((int)(playerStats.curHP * 100 / playerStats.hp));
        playerHPBar.GetComponent<Slider>().value = (int)(playerBS.curHP * 100 / playerBS.hp);
        yield return null;

    }
    public IEnumerator UpdHPEnemyBar(PlayerBattleStats enemyBS)
    {
        //Debug.Log("Current HP: "+ enemyStats.curHP);
        //Debug.Log((int)(playerStats.curHP * 100 / playerStats.hp));
        enemyHPBar.GetComponent<Slider>().value = (int)(enemyBS.curHP * 100 / enemyBS.hp);
        yield return null;

    }
}
