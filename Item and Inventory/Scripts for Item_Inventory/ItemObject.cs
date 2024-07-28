using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //private SpriteRenderer sr;
    [SerializeField]private Rigidbody2D rb;

    [SerializeField] private ItemData itemData;
    //[SerializeField] private Vector2 xVelocity;

    //private void OnValidate()
    //{
    //    SetUpVisual();
    //}

    private void SetUpVisual()
    {
        if (itemData == null)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = itemData.Icon;
        gameObject.name = "Item Object - " + itemData.ItemName;
    }

  

    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    { 
        itemData = _itemData;
        rb.velocity = _velocity;
        SetUpVisual();
    }



    public void PickUpItem()
    {
        if (!Inventory.Instance.CanAddItem() && itemData.ItemType == ItemType.Equipment)  //当角色目前已经无法继续装备物品至装备背包并且拾取的物品类型是装备类型时
        {
            rb.velocity = new Vector2(0, 7); //一点小特效：当角色无法再拾取物品时，掉落的物品会小小跳一下
            PlayerManager.instance.player.fx.CreatePopUpText("背包已满");
            return;
        }

        AudioManager.instance.PlaySfx(18,transform);
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
 