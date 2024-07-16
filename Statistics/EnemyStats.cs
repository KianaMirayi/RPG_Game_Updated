using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Enemy Enemy;
    public ItemDrop DropSystem;

    [Header("Level Detail")]
    [SerializeField] private int level = 1;  //敌人的等级
    [Range(0f,1f)]
    [SerializeField] private float PercentageModifier = 0.4f;


    public override void Start()
    {
        ApplyLevelModifier();//敌人的生命值随等级增加而不断增加

        base.Start();

        Enemy = GetComponent<Enemy>();
        DropSystem = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifier()
    {  // 敌人的数值随等级不断提高
        Modify(Strength);
        Modify(Agility);
        Modify(Intelligence);
        Modify(Vitality);

        Modify(Damage);
        Modify(CritChance);
        Modify(CritPower);

        Modify(MaxHp);
        Modify(Armor);
        Modify(Evasion);
        Modify(MagicResistance);

        Modify(FireDamage);
        Modify(IceDamage);
        Modify(LightingDamage);

    }

    private void Modify(Stats _stats)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stats.GetValue() * PercentageModifier;

            _stats.AddModifier(Mathf.RoundToInt(modifier));  
        }
    }
    
    public override void Update()
    {
        base .Update();

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        //Enemy.DamageImpact();
    }

    public override void Die()
    {
        base.Die();

        Enemy.Die();

        DropSystem.GenerateDrop();
    }
}
