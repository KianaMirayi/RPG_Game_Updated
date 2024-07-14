using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager SkillInstance;  //ʵ�����౾�������農̬���ԣ�ȷ��ȫ��ֻ��һ����ʵ��

    public Player player;


    public Dash_Skill Dash;  //ע��Dash���ܣ�ʹȫ�ֶ�����ͨ���������
    public Clone_Skill Clone;// ע��Clone���ܣ�ʹȫ�ֶ�����ͨ���������
    public Sword_Skill Sword;
    public BlackHole_Skill BlackHole;
    public Crystal_Skill Crystal;



    
    private void Awake()
    {
        if (SkillInstance != null)  //����⵽����ʵ���⻹������ʵ������������ʵ��
        {
            Destroy(SkillInstance.gameObject);
        }
        else  //���򴴽���ʵ��
        {
            SkillInstance = this;
        }
    }

    private void Start()
    {
        Dash = GetComponent<Dash_Skill>();
        Clone = GetComponent<Clone_Skill>();
        Sword = GetComponent<Sword_Skill>();
        BlackHole = GetComponent<BlackHole_Skill>();
        Crystal = GetComponent<Crystal_Skill>();


        player = PlayerManager.instance.player;
    }
}
