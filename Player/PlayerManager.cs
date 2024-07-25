using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;  //单例模式,实例化类本身，并赋予静态属性，保持实例化全局仅有一个该实例

    public Player player;  // 创建Player类成员，这样可以提供全局通过代码访问该类

    public int Currency;

    private void Awake()
    {
        if (instance != null )  //若检测到有其他该类实例则销毁它
        {
            Destroy(instance.gameObject);
        }
        else  //否则创建该实例
        { 
            instance = this;  
        }
    }

    public bool HaveEnoughMoney(int _price)
    {
        if (_price > Currency)
        {
            Debug.Log("勇士很穷没有钱");
            return false;
        }

        Currency = Currency - _price;  //扣钱
        return true;
    }

    public int GetCurrentCurrency() => Currency;



    public void LoadData(GameData _data) //加载游戏时将保存的金币数赋给当前游戏
    {
        Currency = _data.currency;
    }

    public void SaveData(ref GameData _data)  //保存游戏时将现有的金币数保存给GamData
    {
        _data.currency = Currency;
    }
}
