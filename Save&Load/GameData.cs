
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

    public float lostCurrencyX;
    public float lostCurrencyY;
    public int lostCurrencyAmount;

    public SerializableDictionary<string, float> volumeSettings;

    public GameData()
    { 
        this.lostCurrencyX = 0;
        this.lostCurrencyY = 0;
        this.lostCurrencyAmount = 0;

        this.currency = 0; // ����ʼ����Ϸʱ����Ϸ��ʼ����Ϊ0 

        inventory = new SerializableDictionary<string, int>(); //��ʼ����¼װ�����ֵ�
        skillTree = new SerializableDictionary<string, bool>();
        equipmentID = new List<string>();

        closetCheckPointId = string.Empty;
        checkPoints = new SerializableDictionary<string, bool>();


        volumeSettings = new SerializableDictionary<string, float>();


    }


}
