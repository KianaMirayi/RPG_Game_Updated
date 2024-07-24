using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemDescription : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemStory;

    [SerializeField] private int DefaultItemNameFontSize = 28;
    [SerializeField] private int DefaulrItemDescriptonFontSize = 24;


    private ItemData_Equipment itemData_Equipment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemDescription(ItemData_Equipment item)  //获取当前装备的类型以及名字
    {
        if (item == null)
        { 
            return;
        }

        if (item.equipmentType == EquipmentType.Weapon)
        {
            itemTypeText.text = "武器";
        }
        if (item.equipmentType == EquipmentType.Amulet)
        {
            itemTypeText.text = "护身符";
        }
        if (item.equipmentType == EquipmentType.Armor)
        {
            itemTypeText.text = "护甲";
        }
        if (item.equipmentType == EquipmentType.Flask)
        {
            itemTypeText.text = "药水";
        }

        itemNameText.text = item.name;
        //itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();
        //itemStory.text = item.GetDescription();

        if (itemNameText.text.Length > 18)  //当文本字数超过一定限制时，缩小字体
        {
            itemNameText.fontSize = itemNameText.fontSize * 0.8f;
        }
        else
        {
            itemNameText.fontSize = DefaultItemNameFontSize; //28为unity中手动设置的字体大小
        }

        AdjustFontSize(itemDescription);
        AdjustPositionForDescription();

        gameObject.SetActive(true);
    }

    public void HideItemDescription()
    {
        itemNameText.fontSize = DefaultItemNameFontSize;
        itemDescription.fontSize = DefaulrItemDescriptonFontSize;
        gameObject.SetActive(false);
        
    }

    //public void ShowItemStory(ItemData_Equipment _item)
    //{
        

    //    itemStory.text = _item.itemStory;
    //    gameObject.SetActive(true);
    //}

    //public void HideItemStory()
    //{
    //    itemStory.text = "";
    //    gameObject.SetActive(false);
    //}
}
