using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>() != null) //��⵽��Һ͵���ʱ����
        {
            collision.GetComponent<CharacterStats>().KillEntity();

            if (collision.GetComponent<Enemy>() != null)
            {
                Destroy(collision.gameObject, 5);
            }
            
        }
        else
        { 
            Destroy(collision.gameObject); //�������е������������
        }
    }
}
