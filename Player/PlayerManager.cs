using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;  //实例化类本身，并赋予静态属性，保持实例化全局仅有一个该实例

    public Player player;  // 创建Player类成员，这样可以提供全局通过代码访问该类

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
}
