using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private int DefaultFontSize = 28;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemDescription(ItemData_Equipment item)  //��ȡ��ǰװ���������Լ�����
    {
        if (item == null)
        { 
            return;
        }

        itemNameText.text = item.name;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();

        if (itemNameText.text.Length > 18)  //���ı���������һ������ʱ����С����
        {
            itemNameText.fontSize = itemNameText.fontSize * 0.8f;
        }
        else
        {
            itemNameText.fontSize = DefaultFontSize; //28Ϊunity���ֶ����õ������С
        }

        gameObject.SetActive(true);
    }

    public void HideItemDescription()
    {
        itemNameText.fontSize = DefaultFontSize;
        gameObject.SetActive(false);
        
    }

}
