using System.Collections;
using System.Collections.Generic;
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
}
