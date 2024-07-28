using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR  //分配ID给每一个unity中的资产
using UnityEditor;
#endif
public enum ItemType
{
    Material,
    Equipment
}
[CreateAssetMenu(fileName ="New Item Data", menuName ="Data/Item")]
public class ItemData :ScriptableObject
{
    public ItemType ItemType;
    public string ItemName;
    public Sprite Icon;
    public string ItemID;

    [Range(0,100)]
    public float dropChance;


    protected StringBuilder builder = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR  //分配ID给每一个unity中的资产
        string path = AssetDatabase.GetAssetPath(this);
        ItemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
