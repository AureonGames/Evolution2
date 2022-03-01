using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.MultiplayerModels;
using System;
using UnityEngine.UI;
public class MatchMaker : MonoBehaviour
{
    public Text message;


    private void Start()
    {
        Debug.Log("Id de playfab " + PlayFabLogin.pfID);
        PlayFabMultiplayerAPI.CreateMatchmakingTicket(
        new CreateMatchmakingTicketRequest
        {

        // The ticket creator specifies their own player attributes.
        Creator = new MatchmakingPlayer
            {
                Entity = new EntityKey
                {
                    Id = PlayFabLogin.pfID,
                    Type = PlayFabLogin.pfType,
                },

            // Here we specify the creator's attributes.
            Attributes = new MatchmakingPlayerAttributes
                {
                    DataObject = new
                    {
                     //   Skill = 24.4
                    },
                },
            },

        // Cancel matchmaking if a match is not found after 120 seconds.
        GiveUpAfterSeconds = 120,

        // The name of the queue to submit the ticket into.
        QueueName = "PVPBattle",
        },

        // Callbacks for handling success and error.
        this.OnMatchmakingTicketCreated,
        this.OnMatchmakingError
        );
    }


    private void OnMatchmakingError(PlayFabError obj)
    {
       
        print(obj.ErrorMessage);
        print(obj.CustomData);
        throw new NotImplementedException();
    }

    private void OnMatchmakingTicketCreated(CreateMatchmakingTicketResult obj)
    {
        message.text = "Buscando oponente...";
        Debug.Log(obj.Request);
        StartCoroutine(CheckTicketStatus(obj.TicketId));
    }

    IEnumerator CheckTicketStatus(string ticket)
    {
        Debug.Log(ticket);
        yield return new WaitForSeconds(7);
        PlayFabMultiplayerAPI.GetMatchmakingTicket(
    new GetMatchmakingTicketRequest
    {
        TicketId = ticket,
        QueueName = "PVPBattle",
    },
    this.OnGetMatchmakingTicket,
    this.OnMatchmakingError);
    }

    private void OnGetMatchmakingTicket(GetMatchmakingTicketResult obj)
    {

        if (obj.Status != "WaitingForMatch")
        {
            PlayFabMultiplayerAPI.GetMatch(
            new GetMatchRequest
            {

                MatchId = obj.MatchId,
                QueueName = "PVPBattle",
            },
            this.OnGetMatch,
            this.OnMatchmakingError);
        }
        else
        {
            StartCoroutine(CheckTicketStatus(obj.TicketId));
        }
    }

    private void OnGetMatch(GetMatchResult obj)
    {
        throw new NotImplementedException();
    }

}
