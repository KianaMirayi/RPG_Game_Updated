using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI itemText;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem _newitem)
    {
        item = _newitem;

        itemImage.color = Color.white; 

        if (item != null)
        {
            itemImage.sprite = item.data.Icon;

            if (item.StackSize > 1)
            {
                itemText.text = item.StackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void ClearUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        { 
            Inventory.Instance.RemoveItem(item.data);
        }


        if (item.data.ItemType == ItemType.Equipment)
        {

            Inventory.Instance.EquipItem(item.data);
            //Debug.Log("Equipment picked up " + item.data.ItemName);

        }


        //throw new System.NotImplementedException();
    }
}
