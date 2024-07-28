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
        if (!Inventory.Instance.CanAddItem() && itemData.ItemType == ItemType.Equipment)  //����ɫĿǰ�Ѿ��޷�����װ����Ʒ��װ����������ʰȡ����Ʒ������װ������ʱ
        {
            rb.velocity = new Vector2(0, 7); //һ��С��Ч������ɫ�޷���ʰȡ��Ʒʱ���������Ʒ��СС��һ��
            PlayerManager.instance.player.fx.CreatePopUpText("��������");
            return;
        }

        AudioManager.instance.PlaySfx(18,transform);
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
 