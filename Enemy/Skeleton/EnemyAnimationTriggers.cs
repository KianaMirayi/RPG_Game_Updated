using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    public Enemy enemy => GetComponentInParent<Enemy>();

    public void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();

                enemy.stats.DoDamage(target);

                //hit.GetComponent<Player>().DamageImpact();
            }
                  
        }
    }

    public void OpenCounterWindow() => enemy.OpenCounterWindow();
    public void CloseCounterWindow() => enemy.CloseCounterWindow();

    private void SpecialAttackTrigger()
    { 
        enemy.AnimationSpecialAttackTrigger();
    }


    public void PlaySfxForSkeleton()
    {
        AudioManager.instance.PlaySfx(47,null);
    }

    public void PlayAttackSfxForSlime()
    {
        AudioManager.instance.PlaySfx(50,null);
    }

    public void PlaySlimeStunndedSfx()
    {
        AudioManager.instance.PlaySfx(51, null);
    }

    public void PlayShadyMoveSfx()
    {
        AudioManager.instance.PlaySfx(54,null);
    }

    public void PlayExplodeSfxForShady()
    {
        AudioManager.instance.PlaySfx(55, null);
    }

    public void PlayShootArrowForArcher()
    {
        AudioManager.instance.PlaySfx(57,null);
    }

    public void PlayArrowSfx()
    {
        AudioManager.instance.PlaySfx(58, null);
    }
}
