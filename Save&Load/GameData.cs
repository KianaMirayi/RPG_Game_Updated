using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currency;

    public SerializableDictionary<string, int> inventory;//记录装备库存
    public SerializableDictionary<string, bool> skillTree;//记录技能是否已经学习
    public List<string> equipmentID;

    public SerializableDictionary<string, bool> checkPoints;
    public string closetCheckPointId;

    public GameData()
    { 
        this.currency = 0; // 当开始新游戏时，游戏初始货币为0 

        inventory = new SerializableDictionary<string, int>(); //初始化记录装备的字典
        skillTree = new SerializableDictionary<string, bool>();
        equipmentID = new List<string>();

        closetCheckPointId = string.Empty;
        checkPoints = new SerializableDictionary<string, bool>();





    }


}
