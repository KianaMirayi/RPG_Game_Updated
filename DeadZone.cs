using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>() != null) //检测到玩家和敌人时死亡
        {
            collision.GetComponent<CharacterStats>().KillEntity();

            if (collision.GetComponent<Enemy>() != null)
            {
                Destroy(collision.gameObject, 5);
            }
            
        }
        else
        { 
            Destroy(collision.gameObject); //销毁所有掉入该区的物体
        }
    }
}
