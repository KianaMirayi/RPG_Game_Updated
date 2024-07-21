using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType SlotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment Slot _ " + SlotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.data == null)
        { 
            return;
        }

        //base.OnPointerDown(eventData);
        Inventory.Instance.UnEquipItem(item.data as ItemData_Equipment);
        Inventory.Instance.AddItem(item.data as ItemData_Equipment);

        ui.itemDescription.HideItemDescription();

        ClearUpSlot();
    }
}
