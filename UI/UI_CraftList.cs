using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public Transform craftSlotParent;
    [SerializeField] public GameObject craftSlotPrefab;

    [SerializeField] public List<ItemData_Equipment> craftEquipment;
    //[SerializeField] public List<UI_CraftSlot> craftSlots;


    // Start is called before the first frame update
    void Start()
    {
        //AssignCraftSlots();

        transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetUpCraftList();
        SetUpDefaultCraftWindow();
    }

    //private void AssignCraftSlots() //获取该脚本挂载对象下每一个含有UI_CraftSlot的对象并加入craftSlots列表中
    //{
    //    for (int i = 0; i < craftSlotParent.childCount; i++)
    //    {
    //        craftSlots.Add(craftSlotParent.GetChild(i).GetComponent<UI_CraftSlot>());
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpCraftList()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        { 
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }

        //craftSlots = new List<UI_CraftSlot> ();

        for (int i = 0; i < craftEquipment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            newSlot.GetComponent<UI_CraftSlot>().SetUpCraftSlot(craftEquipment[i]);
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }

    public void SetUpDefaultCraftWindow()
    {
        if (craftEquipment[0] != null)
        {
            GetComponentInParent<UI>().craftWindow.SetUpCraftWindow(craftEquipment[0]);
        }
    }

}
