using System;
using System.Collections.Generic;
using UnityEngine;

/*This class will have the data that the player will send to the otherOne in order to determine the current state of the battle*/
[Serializable]
public class BattleStats
{
    public PlayerBattleStats playerStats;
    public PlayerBattleStats enemyStats;

}

/*Borrar: Ya se está enviando la data de cada turno, falta hacer que la animacion de hit llame al player y corregir las barras de hp pq siempre es el enemigo*/


[Serializable]
public class PlayerBattleStats
{
    public int attack;
    public int hp;
    public int magic;
    public int luck;
    public int curAttack;
    public int curHP;
    public int curtLuck;
    public int curMagic;
    public int firstTurn;
    /*Eventualy we can add the id of certain effect like poisoned(turns) forzen(turns) etc.*/
}