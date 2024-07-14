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

        //PlayerManager.instance.player.DamageEffect();
        player.DamageEffect();
    }

    public override void Die()
    {
        base.Die();

        player.Die();
    }
}
