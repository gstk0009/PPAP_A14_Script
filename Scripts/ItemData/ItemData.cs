using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Tutorial,
    PassiveItem
}
public enum PassiveType 
{
    Sprint, //속도를 증가시켜주는 아이템 타입 
    Shield, //일정 시간 동안 무적 상태를 만들어주는 아이템 타입
    None, //아무런 능력 없이 점수만 증가
}

[Serializable]
public class ItemDataType
{
    public PassiveType type;
    public float value; //아이템 지속 시간
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public int itemScore;
    public ItemType type;

    [Header("Passive")]
    public ItemDataType[] passiveTypes;
}

