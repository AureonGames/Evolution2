using System.Collections;
using UnityEngine;

public interface ITurnHandler
{
    /**/
    IEnumerator ShowTurnMessageAnim(string message);
    void Turn(bool yourTurn);
    void BattleResult(int playerCurHP, int enemyCurHP);
    void YouLose();
    void YouWin();
    bool PlayerTurn { get; set;}
    void FirstTurn();
    void TurnStart();
    void ExecuteSkillAnim(Transform caster, Transform tarjet, Skill skill, PlayerBattleStats playerBS, PlayerBattleStats enemyBS);
    IEnumerator HitAnim();
    void DeleteCurrentCards();
    void ShowEnemyTurnMessage();
}