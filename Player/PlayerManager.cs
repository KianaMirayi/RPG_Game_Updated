using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;  //ʵ�����౾�������農̬���ԣ�����ʵ����ȫ�ֽ���һ����ʵ��

    public Player player;  // ����Player���Ա�����������ṩȫ��ͨ��������ʸ���

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
}
