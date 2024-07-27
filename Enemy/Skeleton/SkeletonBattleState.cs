using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    public Enemy_Skeleton skeletonEnemy;

    public Transform player;
    public int moveDir;

    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton enemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        skeletonEnemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().IsDead)
        {
            enemyStateMachine.changeState(skeletonEnemy.skeletonMoveState);
        }
    }
    public override void Update()
    {
        base.Update();

        if (skeletonEnemy.IsPlayerDetected())
        {
            stateTimer = skeletonEnemy.battleTime;

            if (skeletonEnemy.IsPlayerDetected().distance < skeletonEnemy.attackDistance)  //角色在敌人攻击范围内
            {
                if (canAttack())  // 设置骷髅怪的攻击间隔时间
                {
                    enemyStateMachine.changeState(skeletonEnemy.skeletonAttackState);

                }

            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position,skeletonEnemy.transform.position) > 7)  // 战斗状态计时器小于0或者角色与敌人距离过远时进入IdleState
            {
                enemyStateMachine.changeState(skeletonEnemy.skeIdleState); 
            }
        
        }

        if (player.position.x > skeletonEnemy.transform.position.x)
        { 
            moveDir = 1;
        }
        else if (player.position.x < skeletonEnemy.transform.position.x)
        {
            moveDir = -1;
        }

        skeletonEnemy.setVelocity(skeletonEnemy.moveSpeed * moveDir, skeletonEnemy.rb.velocity.y);
        Debug.Log("Skeleton is in battlestate");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool canAttack()
    {
        if (Time.time >= skeletonEnemy.enemyLastAttacked + skeletonEnemy.enemyAttackCoolDown)
        {
            skeletonEnemy.enemyAttackCoolDown = Random.Range(skeletonEnemy.enemyMinAttackCoolDown, skeletonEnemy.enemyMaxAttackCoolDown); //快慢刀
            skeletonEnemy.enemyLastAttacked = Time.time;
            return true;
        }

        Debug.Log("Attack is on cooldown");
        return false;
    }

}
