using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
    public ItemData data;
    public int StackSize;


    public InventoryItem(ItemData _newitemData)
    { 
        data = _newitemData;
        AddStack();
    }

    public void AddStack()
    {
        StackSize++;
    }

    public void RemoveStack()
    { 
        StackSize--;
    }

}
