using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData item;

    [SerializeField] private ItemData[] possibleDrop;
    [SerializeField] private List<ItemData> dropList = new List<ItemData>();
    [SerializeField] private int possibleDropItemAmount;

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            { 
                dropList.Add(possibleDrop[i]);
            }
        }

        for (int i = 0; i < possibleDropItemAmount; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];



            dropList.Remove(randomItem);
            DropItem(randomItem);
        }

    }

    public void DropItem(ItemData _itemData)
    { 
        GameObject newDrop = Instantiate(dropPrefab,transform.position,Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(3, 5),Random.Range(15,20));

        newDrop.GetComponent<ItemObject>().SetUpItem(_itemData, randomVelocity);
    }
}