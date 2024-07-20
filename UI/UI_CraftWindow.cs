using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ItemName;
    [SerializeField] public TextMeshProUGUI ItemDescritpion;
    [SerializeField] public Image ItemIcon;
    [SerializeField] public Button CraftButton;

    [SerializeField] public Image[] MaterialImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpCraftWindow(ItemData_Equipment _data)
    { 
        CraftButton.onClick.RemoveAllListeners();  //?

        for (int i = 0; i < MaterialImage.Length; i++)
        {
            MaterialImage[i].color = Color.clear;
            MaterialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
         
        }

        for (int i = 0; i < _data.CraftingMaterial.Count; i++)
        {
            if (_data.CraftingMaterial.Count > MaterialImage.Length)
            {
                Debug.Log("锻造所需材料数大于锻造格");
            }

            MaterialImage[i].sprite = _data.CraftingMaterial[i].data.Icon;
            MaterialImage[i].color = Color.white;

            TextMeshProUGUI materialSlotText = MaterialImage[i].GetComponentInChildren<TextMeshProUGUI>();
            materialSlotText.text = _data.CraftingMaterial[i].StackSize.ToString();
            materialSlotText.color = Color.white;
        }

        ItemIcon.sprite = _data.Icon;
        ItemName.text = _data.ItemName;  //TODO：待转中文！！！！
        ItemDescritpion.text = _data.GetDescription();

        CraftButton.onClick.AddListener(() => Inventory.Instance.CanCraft(_data, _data.CraftingMaterial));//?

    }

}
