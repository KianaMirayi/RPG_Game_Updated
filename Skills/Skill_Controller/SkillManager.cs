using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager SkillInstance;  //实例化类本身，并赋予静态属性，确保全局只有一个该实例

    public Player player;


    public Dash_Skill Dash;  //注册Dash技能，使全局都可以通过代码访问
    public Clone_Skill Clone;// 注册Clone技能，使全局都可以通过代码访问
    public Sword_Skill Sword;
    public BlackHole_Skill BlackHole;
    public Crystal_Skill Crystal;
    public Parry_Skill Parry;
    public Dodge_Skill Dodge;



    
    private void Awake()
    {
        if (SkillInstance != null)  //若检测到出该实例外还有其他实例则销毁其他实例
        {
            Destroy(SkillInstance.gameObject);
        }
        else  //否则创建该实例
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
        Parry = GetComponent<Parry_Skill>();
        Dodge = GetComponent<Dodge_Skill>();


        player = PlayerManager.instance.player;
    }
}
