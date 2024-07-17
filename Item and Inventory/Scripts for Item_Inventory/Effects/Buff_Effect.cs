using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum StatType
{ 
    strength,
    agility,
    intelligence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicalResistance,
    fireDamage,
    iceDamage,
    lightingDamage
}


[CreateAssetMenu(fileName ="Buff_Effect", menuName = "Data/Buff Effect")]
public class Buff_Effect : ItemEffect
{
    private PlayerStats playerStats;
    [SerializeField] private StatType BuffType;
    [SerializeField] private int BuffAmount;
    [SerializeField] private float BuffDuration;

    public override void ExcuteEffect(Transform _enemyPosition)
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.IncreaseStatBy(BuffAmount, BuffDuration, statsToModify());
    }

    public Stats statsToModify()
    { 
        switch(BuffType)
        {
            case StatType.strength:
                return playerStats.Strength;
                
            case StatType.agility:
                return playerStats.Agility;

            case StatType.intelligence:
                return playerStats.Intelligence;

            case StatType.vitality:
                return playerStats.Vitality;

            case StatType.damage:
                return playerStats.Damage;

            case StatType.critChance:
                return playerStats.CritChance;

            case StatType.critPower:
                return playerStats.CritPower;

            case StatType.health:
                return playerStats.MaxHp;

            case StatType.armor:
                return playerStats.Armor;

            case StatType.evasion:
                return playerStats.Evasion;

            case StatType.magicalResistance:
                return playerStats.MagicResistance;

            case StatType.fireDamage:
                return playerStats.FireDamage;

            case StatType.iceDamage:
                return playerStats.IceDamage;

            case StatType.lightingDamage:
                return playerStats.LightingDamage;



            default:
                return null;

        }
    }

}
