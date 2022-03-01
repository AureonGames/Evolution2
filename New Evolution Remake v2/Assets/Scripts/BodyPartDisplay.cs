using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBodyCard : MonoBehaviour
{
    public string bodyPart;
    SO_DB db;

    public void ChangeSelection(string newBodyPart)
    {
        if (bodyPart != newBodyPart)
        {
            if (newBodyPart == "Head")
            {
                bodyPart = "Head";

            }
        }
    }
}
