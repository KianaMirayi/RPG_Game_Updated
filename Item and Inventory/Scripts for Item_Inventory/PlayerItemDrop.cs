using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drop")]
    [SerializeField] private float chanceToLoseItem;  // ��ɫ��������װ���ĸ���
    [SerializeField] private float chanceToLoseMaterial;

    public override void GenerateDrop()
    {
        Inventory _inventory = Inventory.Instance;

        List<InventoryItem> currentEquipment = _inventory.GetEquipmentList();  //��ȡ��ҵ�ǰ��װ���б�
        List<InventoryItem> currentStash = _inventory.GetStashItemList();  //��ȡ��ҵ�ǰ�Ŀ��/�����б�
        List<InventoryItem> equipmentToLose = new List<InventoryItem>();  //��ɫ����װ������
        List<InventoryItem> materialToLose = new List<InventoryItem>();  //��ɫ�������ϵ���

        //��ɫ��װ������
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


        //��ɫ�Ĳ��ϵ���
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
