using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NetworkMessage 
{
    string sender;
    string opcode;
    string data1;
    string data2;
    string data3;

    public NetworkMessage()
    {
    }

    public NetworkMessage(string sender, string opcode, string data1, string data2, string data3)
    {
        this.sender = sender;
        this.opcode = opcode;
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
    }

    public string Sender { get => sender; set => sender = value; }
    public string Opcode { get => opcode; set => opcode = value; }
    public string Data1 { get => data1; set => data1 = value; }
    public string Data2 { get => data2; set => data2 = value; }
    public string Data3 { get => data3; set => data3 = value; }
}
