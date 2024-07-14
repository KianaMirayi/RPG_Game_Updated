using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Enemy Enemy;
    public override void Start()
    {
        base.Start();

        Enemy = GetComponent<Enemy>();
    }

    
    public override void Update()
    {
        base .Update();

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        Enemy.DamageEffect();
    }

    public override void Die()
    {
        base.Die();

        Enemy.Die();
    }
}
