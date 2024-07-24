using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemData> StartingEquipment;  //初始装备

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary <ItemData, InventoryItem> stashDictionary;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;  //背包界面
    [SerializeField] private Transform stashSlotParent; //材料界面
    [SerializeField] private Transform equipmentSlotParent;//装备界面
    [SerializeField] private Transform statSlotParent;//角色数据

    private UI_ItemSlot[] inventoryItemSlot;//背包界面数组
    private UI_ItemSlot[] stashItemSlot;//材料界面数组
    private UI_EquipmentSlot[] equipmentSlot;//装备界面数组
    private UI_StatSlot[] statSlot;//角色界面数组

    [Header("Items CoolDown")]
    private float lastTimeUsedFlask;
    private float lastTimeUsedArmor;

    public  float FlaskCoolDown { get; private set; }
    private float ArmorCoolDown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();


        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();


        AddStartingItem();

    }

    private void AddStartingItem()  //初始装备
    {
        for (int i = 0; i < StartingEquipment.Count; i++)
        {
            if (StartingEquipment[i] != null)  //避免填充初始装备时没有指定具体的装备
            { 
                AddItem(StartingEquipment[i]);
                
            }

        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;

        InventoryItem newitem = new InventoryItem(newEquipment);


        ItemData_Equipment OldEquipment = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                OldEquipment = item.Key;
            }
        }

        if (OldEquipment != null)
        { 
            UnEquipItem(OldEquipment);
            AddItem(OldEquipment);


        }


        equipment.Add(newitem);
        equipmentDictionary.Add(newEquipment, newitem);
        newEquipment.AddModifiers();

        RemoveItem(_item);

        //UpdateSlotUI(); //已经在RemoveItem AddItem中实现

    }

    public void UnEquipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            ;
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].SlotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].ClearUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].ClearUpSlot();
        }


        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }

        UpDateStatsUI();

    }

    public void UpDateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public bool CanAddItem()//当角色收集装备数大于当前角色装备背包容量时
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            Debug.Log("No More Space");
            return false;
        }
        return true;
    }
    public void AddItem(ItemData _item)
    {
        if (_item.ItemType == ItemType.Equipment && CanAddItem())
        {
            AddToInventory(_item);

        }
        else if (_item.ItemType == ItemType.Material)
        {
            AddToStash(_item);
        }


        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)  
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            stash.Add(newitem);
            stashDictionary.Add(_item, newitem);
        }
    }
    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            inventory.Add(newitem);
            inventoryDictionary.Add(_item, newitem);
        }
    }

    public void RemoveItem(ItemData _item)
    { 
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {

            if (value.StackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            { 
                value.RemoveStack();
            }
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashvalue))
        {
            if (stashvalue.StackSize <= 1)
            {
                stash.Remove(stashvalue);
                stashDictionary.Remove(_item);
            }
            else
            {
                stashvalue.RemoveStack();
            }
        }


        UpdateSlotUI();
    }



    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)  //合成台工艺
    { 
        List<InventoryItem> materialToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))
            {
                if (stashValue.StackSize < _requiredMaterials[i].StackSize)
                {
                    Debug.Log("Not enough materials");
                    return false;
                }
                else
                { 
                    materialToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("Not enough materials");
                return false;
            }
        }


        for (int i = 0; i < materialToRemove.Count; i++)
        {
            RemoveItem(materialToRemove[i].data);
        }

        AddItem(_itemToCraft);
        Debug.Log("Here is your item " + _itemToCraft.name);

        return true;


    }

    public List<InventoryItem> GetEquipmentList() => equipment;
    public List<InventoryItem> GetStashItemList() => stash;

    public ItemData_Equipment GetTypeOfEquipment(EquipmentType _type)
    {   
        ItemData_Equipment equipedItem = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
            { 
                equipedItem = item.Key;
            }
        }

        return equipedItem;
    }

    public void UseFlask()
    { 
        ItemData_Equipment currentFlask = GetTypeOfEquipment(EquipmentType.Flask);

        if (currentFlask == null)
        {
            return;
        }

        bool canUseFlask = Time.time > lastTimeUsedFlask + FlaskCoolDown;

        if (canUseFlask)
        {
            FlaskCoolDown = currentFlask.itemCoolDown;
            currentFlask.Effect(null);
            lastTimeUsedFlask = Time.time;
        }
        else
        {
            Debug.Log("Flask On CoolDown");
        }
        
    }

    public bool CanUseArmor()
    { 
        ItemData_Equipment currentArmor = GetTypeOfEquipment(EquipmentType.Armor);

        if (Time.time > lastTimeUsedArmor + ArmorCoolDown)
        {
            ArmorCoolDown = currentArmor.itemCoolDown;
            lastTimeUsedArmor = Time.time;
            return true;
        }

        Debug.Log("Armor on cooldown");
        return false;
    }



}


