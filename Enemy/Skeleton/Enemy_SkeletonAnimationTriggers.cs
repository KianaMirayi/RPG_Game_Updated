using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
{
    public Enemy_Skeleton skeletonEnemy => GetComponentInParent<Enemy_Skeleton>();

    public void AnimationTrigger()
    {
        skeletonEnemy.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeletonEnemy.attackCheck.position, skeletonEnemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();

                skeletonEnemy.stats.DoDamage(target);

                //hit.GetComponent<Player>().DamageEffect();
            }
                  
        }
    }

    public void OpenCounterWindow() => skeletonEnemy.OpenCounterWindow();
    public void CloseCounterWindow() => skeletonEnemy.CloseCounterWindow();

}
