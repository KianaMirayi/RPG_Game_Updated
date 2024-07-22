using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}


[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Unique effect")]
    public ItemEffect[] itemEffects;
    public float itemCoolDown;

    [TextArea]
    [SerializeField] public string EffectDescription;


    [Header("Major Stats")]
    public int Strength;
    public int Agility;
    public int Intelligence;
    public int Vitality;

    [Header("Offensive Stats")]
    public int Damage;
    public int CritChance;
    public int CritePower;

    [Header("Defensive Stats")]
    public int Health;
    public int Armor;
    public int Evasion;
    public int MagicalResistance;

    [Header("Magic Stats")]
    public int FireDamage;
    public int IceDamage;
    public int LightingDamage;

    [Header("Craft Requirement")]
    public List<InventoryItem> CraftingMaterial;


    private int DescriptionLength;

    public void Effect(Transform _transform)
    {
        foreach (var item in itemEffects)
        { 
            item.ExcuteEffect(_transform);
        }
    }



    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.Strength.AddModifier(Strength);
        playerStats.Agility.AddModifier(Agility);
        playerStats.Intelligence.AddModifier(Intelligence);
        playerStats.Vitality.AddModifier(Vitality);

        playerStats.Damage.AddModifier(Damage);
        playerStats.CritChance.AddModifier(CritChance);
        playerStats.CritPower.AddModifier(CritePower);

        playerStats.MaxHp.AddModifier(Health);
        playerStats.Armor.AddModifier(Armor);
        playerStats.Evasion.AddModifier(Evasion);
        playerStats.MagicResistance.AddModifier(MagicalResistance);

        playerStats.FireDamage.AddModifier(FireDamage);
        playerStats.IceDamage.AddModifier(IceDamage);
        playerStats.LightingDamage.AddModifier(LightingDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.Strength.RemoveModifier(Strength);
        playerStats.Agility.RemoveModifier(Agility);
        playerStats.Intelligence.RemoveModifier(Intelligence);
        playerStats.Vitality.RemoveModifier(Vitality);

        playerStats.Damage.RemoveModifier(Damage);
        playerStats.CritChance.RemoveModifier(CritChance);
        playerStats.CritPower.RemoveModifier(CritePower);

        playerStats.MaxHp.RemoveModifier(Health);
        playerStats.Armor.RemoveModifier(Armor);
        playerStats.Evasion.RemoveModifier(Evasion);
        playerStats.MagicResistance.RemoveModifier(MagicalResistance);

        playerStats.FireDamage.RemoveModifier(FireDamage);
        playerStats.IceDamage.RemoveModifier(IceDamage);
        playerStats.LightingDamage.RemoveModifier(LightingDamage);
    }

    public override string GetDescription()
    {
        builder.Length = 0;
        DescriptionLength = 0;

        AddItemDescription(Strength, "Á¦Á¿");
        AddItemDescription(Agility, "ÌåÁ¦");
        AddItemDescription(Intelligence, "ÖÇÁ¦");

        AddItemDescription(Damage, "¹¥»÷Á¦");
        AddItemDescription(CritChance, "±©»÷ÂÊ");
        AddItemDescription(CritePower, "±©»÷ÉËº¦");

        AddItemDescription(Health, "ÉúÃüÖµ");
        AddItemDescription(Armor, "»¤¼×");
        AddItemDescription(Evasion, "ÉÁ±Ü");
        AddItemDescription(MagicalResistance, "Ä§·¨¿¹ÐÔ");

        AddItemDescription(FireDamage, "»ðÔªËØÉËº¦");
        AddItemDescription(IceDamage, "±ùÔªËØÉËº¦");
        AddItemDescription(LightingDamage, "À×ÔªËØÉËº¦");


        if (DescriptionLength < 5)
        {
            for (int i = 0; i < 5 - DescriptionLength; i++)
            {
                builder.AppendLine();
                
                
                
                builder.Append("123");
            }
        }

        if (EffectDescription.Length > 0)
        {
            builder.AppendLine();
            builder.AppendLine();

            builder.Append(EffectDescription);
        }

        return builder.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (builder.Length > 0)
            {
                builder.AppendLine();
            }
        }

        if (_value > 0)
        {

            builder.Append("+ " + _value + " " + _name);
        }

        DescriptionLength++;
    }

    
}
