using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class UI_ItemSlot : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
