using UnityEngine;

public interface IBodyDisplay
{
    void UpdateBodyPart(SO_DB.ItemData item);
    void InitialEquip(SO_DB.PlayerData db);
    void SetPD(SO_DB.PlayerData playerData);
    Animator[] GetAnimators();
}