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
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
 