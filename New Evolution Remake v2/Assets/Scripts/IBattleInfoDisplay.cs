using System.Collections;

public interface IBattleInfoDisplay
{
    IEnumerator UpdHPPlayerBar(PlayerBattleStats playerBS);
    IEnumerator UpdHPEnemyBar(PlayerBattleStats enemyBS);
}