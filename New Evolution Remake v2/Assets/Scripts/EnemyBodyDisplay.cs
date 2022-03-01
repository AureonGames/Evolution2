using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyDisplay : MonoBehaviour, IBodyDisplay
{
    /*used to storage the bodyparts While you change Chest*/
    public Transform head;
    public Transform rightLeg;
    public Transform leftLeg;
    public Transform rightArm;
    public Transform leftArm;
    public Transform chest;
    public Transform back;

    /*use for Chest pivot*/
    public Transform spritesPivot;


    public Transform chestHead;
    public Transform chestRightLeg;
    public Transform chestLeftLeg;
    public Transform chestRightArm;
    public Transform chestLeftArm;

    public SO_DB db;
    public SO_DB.PlayerData pd;

    public void SetPD(SO_DB.PlayerData playerData)
    {
        pd = playerData;
    }

    public Animator[] GetAnimators()
    {
        return spritesPivot.GetComponentsInChildren<Animator>();
    }

    /*This method will set the inital equip registred in the DB_SO*/
    public void InitialEquip(SO_DB.PlayerData playerData)
    {
        Debug.Log("Cargando Equipo inicial");
        /*Fist we have to set the equiped chest in order to get the pivots*/
        /*we asume nothing is instantiated on the first load*/
        foreach (SO_DB.ItemData item in playerData.items)
        {
            if (item.equiped && item.equip.bodyType == "Chest")
            {
                /*UpdateBodyPart will only instantiate the chest*/
                UpdateBodyPart(item);
            }
        }
        /*All the bodyparts but Chest*/
        /*Finds all the equiped items that are not the chest*/
        foreach (SO_DB.ItemData item in playerData.items)
        {
            if (item.equiped && item.equip.bodyType != "Chest")
            {
                UpdateBodyPart(item);
            }
        }
    }


    /*This method will call the bodypart depending on te equip type*/
    public void UpdateBodyPart(SO_DB.ItemData item)
    {
        Equip equip = item.equip;
        if (equip.bodyType == "Head")
        {

            SetBodyPart("Head", equip.bodyPart, head, chestHead);
        }
        if (equip.bodyType == "Arm")
        {
            SetBodyPart("LeftArm", equip.bodyPart, leftArm, chestLeftArm);
            SetBodyPart("RightArm", equip.bodyPart, rightArm, chestRightArm);

        }
        if (equip.bodyType == "Leg")
        {
            SetBodyPart("LeftLeg", equip.bodyPart, leftLeg, chestLeftLeg);
            SetBodyPart("RightLeg", equip.bodyPart, rightLeg, chestRightLeg);

        }
        if (equip.bodyType == "Chest")
        {
            SetBodyPart(equip.bodyPart, spritesPivot);
        }


        GetChestPivots(chest);
        SetChestPivotPositions(chest);
    }




    /*This method will save the new chest pivots in variables, it's called when you change the chest*/
    /*if its the first time  */

    void SetChestPivotPositions(Transform ch)
    {
        Transform bodypart;

        /*Head*/
        if (head.childCount > 0)
        {
            bodypart = head.GetChild(0);
            bodypart.parent = chestHead;
            bodypart.localPosition = Vector3.zero;
        }
        /*RightLeg*/
        if (rightLeg.childCount > 0)
        {
            bodypart = rightLeg.GetChild(0);
            bodypart.parent = chestRightLeg;
            bodypart.localPosition = Vector3.zero;
        }
        /*LeftLeg*/
        if (leftLeg.childCount > 0)
        {
            bodypart = leftLeg.GetChild(0);
            bodypart.parent = chestLeftLeg;
            bodypart.localPosition = Vector3.zero;
        }
        /*RightArm*/
        if (rightArm.childCount > 0)
        {
            bodypart = rightArm.GetChild(0);
            bodypart.parent = chestRightArm;
            bodypart.localPosition = Vector3.zero;
        }
        /*LeftArm*/
        if (leftArm.childCount > 0)
        {

            bodypart = leftArm.GetChild(0);
            bodypart.parent = chestLeftArm;
            bodypart.localPosition = Vector3.zero;
        }
        ch.localPosition = Vector3.zero;

    }

    /*This method will unset the equip from the chest so they dont destroy along with the chest*/
    /*It will only be called when you change chest bodypart*/
    void UnassingEquipFromChest(Transform bodyPart)
    {
        /*Get all the pivots from the chest*/
        List<ChestPivot.Pivot> pivots = bodyPart.GetComponent<ChestPivot>().pivots;

        foreach (ChestPivot.Pivot pivot in pivots)
        {
            if (pivot.name == "Head" && chestHead.childCount > 0)
            {

                chestHead.GetChild(0).SetParent(head);
            }
            if (pivot.name == "LeftArm" && chestLeftArm.childCount > 0)
            {
                chestLeftArm.GetChild(0).SetParent(leftArm);

                Debug.Log(leftArm.GetChild(0).name);
            }
            if (pivot.name == "RightArm" && chestRightArm.childCount > 0)
            {
                chestRightArm.GetChild(0).SetParent(rightArm);
            }

            if (pivot.name == "LeftLeg" && chestLeftLeg.childCount > 0)
            {
                chestLeftLeg.GetChild(0).SetParent(leftLeg);
            }

            if (pivot.name == "RightLeg" && chestRightLeg.childCount > 0)
            {
                chestRightLeg.GetChild(0).SetParent(rightLeg);
            }
        }
        Debug.Log("Aqui estoy saliendo a desequipar: ");
    }



    /*Special for Chest */
    /**/
    void SetBodyPart(Transform bodyPart, Transform bodyPartHolder)
    {
        if (chest)
        {
            /*Step 1 : Set all the bodypart children in variables*/
            UnassingEquipFromChest(chest);
            /*Destroys the current chest transform*/
            Destroy(chest.gameObject);
            /*Instantiates the new transform*/
            chest = Instantiate(bodyPart, bodyPartHolder);
            /*Set the new chest pivots in the chest variables*/
            GetChestPivots(bodyPart);
            /*get the variables from step 1 put the transform in the chest positions*/
        }
        else
        {
            /*First load, the chest prefab becomes the new chest Transform*/
            chest = Instantiate(bodyPart, bodyPartHolder);
            GetChestPivots(bodyPart);
        }
    }


    /*This method will instantiate the new boydpart and attach it to the chest pivot or will instantiate the new chest bodypart*/
    void SetBodyPart(string bodyType, Transform bodyPart, Transform bodyPartHolder, Transform chestBodyPartHolder)
    {

        /*Si existe una piza, la elimino primero*/
        if (bodyPartHolder.childCount > 0) Debug.Log("Este es el equipo " + bodyPartHolder.GetChild(0).name);

        if (bodyPartHolder.childCount > 0)
        {
            Destroy(bodyPartHolder.GetChild(0).gameObject);
        }
        else if (chestBodyPartHolder.childCount > 0)
        {
            Destroy(chestBodyPartHolder.GetChild(0).gameObject);
        }
        Transform newPart = Instantiate(bodyPart, bodyPartHolder);
        newPart.localPosition = Vector3.zero;


        // This is to make the left bodypart to go in front of the rest
        if (bodyType == "LeftArm" || bodyType == "LeftLeg") newPart.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "LeftBodyPart";

    }

    void GetChestPivots(Transform bodyPart)
    {
        foreach (ChestPivot.Pivot pivot in bodyPart.GetComponent<ChestPivot>().pivots)
        {

            if (pivot.name == "Head") chestHead = pivot.pivot;
            if (pivot.name == "RightLeg") chestRightLeg = pivot.pivot;
            if (pivot.name == "LeftLeg") chestLeftLeg = pivot.pivot;
            if (pivot.name == "RightArm") chestRightArm = pivot.pivot;
            if (pivot.name == "LeftArm") chestLeftArm = pivot.pivot;



        }
    }


}
