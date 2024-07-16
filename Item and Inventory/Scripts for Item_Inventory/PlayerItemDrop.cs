using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drop")]
    [SerializeField] private float chanceToLoseItem;  // 角色死亡掉落装备的概率
    [SerializeField] private float chanceToLoseMaterial;

    public override void GenerateDrop()
    {
        Inventory _inventory = Inventory.Instance;

        List<InventoryItem> currentEquipment = _inventory.GetEquipmentList();  //获取玩家当前的装备列表
        List<InventoryItem> currentStash = _inventory.GetStashItemList();  //获取玩家当前的库存/材料列表
        List<InventoryItem> equipmentToLose = new List<InventoryItem>();  //角色死亡装备掉落
        List<InventoryItem> materialToLose = new List<InventoryItem>();  //角色死亡材料掉落

        //角色的装备掉落
        foreach (InventoryItem equipmentItem in currentEquipment)
        {
            if (Random.Range(0, 100) <= chanceToLoseItem)
            {
                DropItem(equipmentItem.data);
                equipmentToLose.Add(equipmentItem);
            }
        }

        for (int i = 0; i < equipmentToLose.Count; i++)
        {
            _inventory.UnEquipItem(equipmentToLose[i].data as ItemData_Equipment);
            
        }


        //角色的材料掉落
        foreach (InventoryItem stashItem in currentStash)
        {
            if (Random.Range(0, 100) <= chanceToLoseMaterial)
            {
                DropItem(stashItem.data);
                materialToLose.Add(stashItem);

            }
        }

        for (int i = 0; i < materialToLose.Count; i++)
        {
            _inventory.RemoveItem(materialToLose[i].data);
        }


    }

}
