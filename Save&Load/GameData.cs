using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currency;

    public SerializableDictionary<string, int> inventory;//��¼װ�����
    public SerializableDictionary<string, bool> skillTree;//��¼�����Ƿ��Ѿ�ѧϰ
    public List<string> equipmentID;

    public SerializableDictionary<string, bool> checkPoints;
    public string closetCheckPointId;

    public GameData()
    { 
        this.currency = 0; // ����ʼ����Ϸʱ����Ϸ��ʼ����Ϊ0 

        inventory = new SerializableDictionary<string, int>(); //��ʼ����¼װ�����ֵ�
        skillTree = new SerializableDictionary<string, bool>();
        equipmentID = new List<string>();

        closetCheckPointId = string.Empty;
        checkPoints = new SerializableDictionary<string, bool>();





    }


}
