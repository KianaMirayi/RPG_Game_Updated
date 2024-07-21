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

    public virtual void OnPointerDown(PointerEventData eventData)  //IPointerDownHandler �� Unity �����е�һ���ӿڣ����ڴ���ָ�밴�£�������¼������� Unity �� UI �¼�ϵͳ��һ���֣�ͨ���� EventTrigger ������ UI ���һ��ʹ�ã��Լ�����Ӧ�û������롣
    {

        if (item == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl)) //��ס��control��ɾ�������е�װ��
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

    public void OnPointerEnter(PointerEventData eventData)//OnPointerEnter �� Unity �����е�һ���ӿڷ��������ڴ���ָ����루��ͣ���¼������� IPointerEnterHandler �ӿڵ�һ���֣������ڼ�⵱��������ָ���豸��ͣ����Ϸ�����ϵ�ʱ��
    {
        if (item == null)
        { 
            return ;
        }

        ui.itemDescription.ShowItemDescription(item.data as ItemData_Equipment);
    }

    public void OnPointerExit(PointerEventData eventData)//OnPointerExit �� Unity �����е�һ���ӿڷ��������ڴ���ָ���˳����뿪���¼������� IPointerExitHandler �ӿڵ�һ���֣������ڼ�⵱��������ָ���豸�뿪��Ϸ�����ʱ��
    {
        if (item == null)
        {
            return;
        }

        ui.itemDescription.HideItemDescription();
    }
}
