using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{

    public override void Start()
    {
        base.Start();
    }

    //private void OnEnable()
    //{
    //    UpdateSlot(item);
    //}

    public void SetUpCraftSlot(ItemData_Equipment _data)
    {
        if (_data == null)
        {
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.Icon;

        //if (_data.equipmentType == EquipmentType.Weapon)
        //{
        //    itemText.text = "武器";
        //}
        //if (_data.equipmentType == EquipmentType.Amulet)
        //{
        //    itemText.text = "护身符";
        //}
        //if (_data.equipmentType == EquipmentType.Armor)
        //{
        //    itemText.text = "护甲";
        //}
        //if (_data.equipmentType == EquipmentType.Flask)
        //{
        //    itemText.text = "药水";
        //}
        

        itemText.text = _data.ItemName;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //ItemData_Equipment craftData = item.data as ItemData_Equipment;

        //Inventory.Instance.CanCraft(craftData, craftData.CraftingMaterial);

        ui.craftWindow.SetUpCraftWindow(item.data as ItemData_Equipment);
    }
}
