using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]public Image itemImage;
    [SerializeField]public TextMeshProUGUI itemText;

    [SerializeField] public TextMeshProUGUI ItemStory;

    private ItemData_Equipment itemData_Equipment;

    public InventoryItem item;

    public UI ui;

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

    public virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)  //IPointerDownHandler 是 Unity 引擎中的一个接口，用于处理指针按下（点击）事件。它是 Unity 的 UI 事件系统的一部分，通常与 EventTrigger 和其他 UI 组件一起使用，以检测和响应用户的输入。
    {

        if (item == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl)) //按住左control键删除背包中的装备
        { 
            Inventory.Instance.RemoveItem(item.data);
            return;
        }


        if (item.data.ItemType == ItemType.Equipment)
        {

            Inventory.Instance.EquipItem(item.data);
            //Debug.Log("Equipment picked up " + item.data.ItemName);

        }

        ui.itemDescription.HideItemDescription();


        //throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)//OnPointerEnter 是 Unity 引擎中的一个接口方法，用于处理指针进入（悬停）事件。它是 IPointerEnterHandler 接口的一部分，常用于检测当鼠标或其他指针设备悬停在游戏对象上的时候。
    {
        if (item == null)
        { 
            return ;
        }

        Vector2 mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 600)
        {
            xOffset = -250;
        }
        else
        {
            xOffset = 250;
        }

        if (mousePosition.y > 500)
        {
            yOffset = -100;
        }
        else
        {
            yOffset = 135;
        }

        ui.itemDescription.ShowItemDescription(item.data as ItemData_Equipment);
        ui.itemDescription.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
        //ui.itemDescription.ShowItemStory(itemData_Equipment);
        


    }

    public void OnPointerExit(PointerEventData eventData)//OnPointerExit 是 Unity 引擎中的一个接口方法，用于处理指针退出（离开）事件。它是 IPointerExitHandler 接口的一部分，常用于检测当鼠标或其他指针设备离开游戏对象的时候。
    {
        if (item == null)
        {
            return;
        }

        ui.itemDescription.HideItemDescription();
        //ui.itemDescription.HideItemStory();
        
    }
}
