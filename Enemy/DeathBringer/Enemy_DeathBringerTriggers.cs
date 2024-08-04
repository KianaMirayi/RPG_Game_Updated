using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathBringerTriggers : EnemyAnimationTriggers
{
    private Enemy_DeathBringer deathBringer => GetComponentInParent<Enemy_DeathBringer>();

    private void Relocate() => deathBringer.FindPosition();

    private void MakeInVisible() => deathBringer.fx.MakeTransparent(true);
    private void MakeVisible() => deathBringer.fx.MakeTransparent(false);

    public void PlayAttackSfx1()
    {
        AudioManager.instance.PlaySfx(62, null);
    }

    public void PlayAttackSfx2()
    {
        AudioManager.instance.PlaySfx(61, null);
    }

    public void PlayCastSfx()
    {
        AudioManager.instance.PlaySfx(63, null);
    }

    public void BeginTeleport()
    {
        AudioManager.instance.PlaySfx(66, null);
    }

    public void ExitTeleport()
    {
        AudioManager.instance.PlaySfx(67, null);
    }
}
