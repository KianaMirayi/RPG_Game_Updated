using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    public ItemObject itemObject => GetComponentInParent<ItemObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().IsDead)  //��ɫ����ʱ���ɼ��������װ���ϵ���ĵ���
            {
                return;
            }

            itemObject.PickUpItem();
        }
    }
}
