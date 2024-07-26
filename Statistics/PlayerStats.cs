using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    public override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        //PlayerManager.instance.player.DamageImpact();
        //player.DamageImpact();
    }


    public override void DecreaseHpBy(int _damage)
    {
        base.DecreaseHpBy(_damage);

        ItemData_Equipment currentArmor = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Armor);

        if (currentArmor != null)
        {
            currentArmor.Effect(player.transform);
        }
    }

    public override void Die()
    {
        base.Die();

        player.Die();

        GameManager.instance.lostCurrencyAmount = PlayerManager.instance.Currency;
        PlayerManager.instance.Currency = 0;

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }


    public override void OnEvasion()
    {
        //Debug.Log("Player avoided attack");
        SkillManager.SkillInstance.Dodge.CreatMirageOnDodge();
    }

    public void CloneDoDamage(CharacterStats _targetStats, float _attackMultiplier)
    {
        if (TargetCanAvoidAttack(_targetStats))
        {
            return;
        }

        int totalDamage = Damage.GetValue() + Strength.GetValue();  //总伤害 = 基础伤害 + 力量加点

        if (_attackMultiplier > 0) //应用幻影的伤害乘数
        {
            totalDamage = Mathf.RoundToInt(totalDamage * _attackMultiplier);
        }

        if (CanCrit())
        {
            //Debug.Log("Critical HIT");
            totalDamage = CalculateCriticalDamage(totalDamage);
            //Debug.Log("Total Critical Damage is" + totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);
        //DoMagicDamage(_target);
    }


}
