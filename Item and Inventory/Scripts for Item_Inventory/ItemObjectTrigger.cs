using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    public ItemObject itemObject => GetComponentInParent<ItemObject>();
    public EntityFX fx => GetComponent<EntityFX>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().IsDead)  //角色死亡时不可捡起从自身装备上掉落的道具
            {
                return;
            }

            itemObject.PickUpItem();
            //fx.CreatePopUpText(itemObject.gameObject.name);
        }
    }
}
