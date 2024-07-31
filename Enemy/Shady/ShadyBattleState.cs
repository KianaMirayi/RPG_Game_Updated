using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyBattleState : EnemyState
{
    public Enemy_Shady shady;

    public Transform player;

    public int moveDir;

    private float shadyDefaultSpeed;

    public ShadyBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Shady shady) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.shady = shady;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        shadyDefaultSpeed = shady.moveSpeed;

        shady.moveSpeed = shady.battleStateMoveSpeed;

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().IsDead)
        {
            enemyStateMachine.changeState(shady.shadyMoveState);
        }
    }
    public override void Update()
    {
        base.Update();

        if (shady.IsPlayerDetected())
        {
            stateTimer = shady.battleTime;

            if (shady.IsPlayerDetected().distance < shady.attackDistance)  //角色在敌人攻击范围内
            {
                //enemyStateMachine.changeState(shady.shadyDeadState);
                shady.stats.KillEntity(); //进入死亡状态，并且可以触发爆炸和掉落

                //if (canAttack())  // 设置骷髅怪的攻击间隔时间
                //{
                //    enemyStateMachine.changeState(shady.skeletonAttackState);

                //}

            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position, shady.transform.position) > 7)  // 战斗状态计时器小于0或者角色与敌人距离过远时进入IdleState
            {
                enemyStateMachine.changeState(shady.shadyIdleState);
            }

        }

        if (player.position.x > shady.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < shady.transform.position.x)
        {
            moveDir = -1;
        }

        shady.setVelocity(shady.moveSpeed * moveDir, shady.rb.velocity.y);
        Debug.Log("is in battlestate");
    }

    public override void Exit()
    {
        base.Exit();

        shady.moveSpeed = shadyDefaultSpeed;
    }

    public bool canAttack()
    {
        if (Time.time >= shady.enemyLastAttacked + shady.enemyAttackCoolDown)
        {
            shady.enemyAttackCoolDown = Random.Range(shady.enemyMinAttackCoolDown, shady.enemyMaxAttackCoolDown); //快慢刀
            shady.enemyLastAttacked = Time.time;
            return true;
        }

        Debug.Log("Attack is on cooldown");
        return false;
    }

}
