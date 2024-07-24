using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;  //ʵ�����౾�������農̬���ԣ�����ʵ����ȫ�ֽ���һ����ʵ��

    public Player player;  // ����Player���Ա�����������ṩȫ��ͨ��������ʸ���

    public int Currency;

    private void Awake()
    {
        if (instance != null )  //����⵽����������ʵ����������
        {
            Destroy(instance.gameObject);
        }
        else  //���򴴽���ʵ��
        { 
            instance = this;  
        }
    }

    public bool HaveEnoughMoney(int _price)
    {
        if (_price > Currency)
        {
            Debug.Log("��ʿ����û��Ǯ");
            return false;
        }

        Currency = Currency - _price;  //��Ǯ
        return true;
    }

    public int GetCurrentCurrency() => Currency;
}
