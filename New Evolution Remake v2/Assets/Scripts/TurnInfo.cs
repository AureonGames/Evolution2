using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnInfo
{
    public BattleStats bs;
    public string skillID;


    public void Awake()
    {
        bs = new BattleStats();
    }
}


/*quede aqui, debo enviar esta estrucura cuando envio los datos del jugador
 *pasos faltantes
 * listo -- Hacer que esta sea la estructura que lee el proceso que convierte a JSON
 *  Ajustar la animacion del personaje y llamarlo por el turnHandler
 *  
 *  Juego la carta
 *  calculo efectos
 *  comienzo animacion de player
 *  al contacto con el enemigo:
 *      Ejecuto la animacion de impacto
 *      ejecuto el update de la barra vida del enemigo
 *      y mi barra de vida
 *  al mismo tiempo envio datos al usuario del turno
 *  al recibir la informacion de los stats y el id de la skill hago lo mismo pero con los Transform y datas al reves
 */