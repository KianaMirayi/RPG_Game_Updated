using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //private SpriteRenderer sr;


    [SerializeField] private ItemData itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.Icon;
        gameObject.name = "Item Object - " + itemData.ItemName;
    }

    //private void Start()
    //{
    //    sr = GetComponent<SpriteRenderer>();

    //    sr.sprite = itemData.Icon;

    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Inventory.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
 