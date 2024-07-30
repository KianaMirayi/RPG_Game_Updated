using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBattleState : EnemyState
{
    public Enemy_Archer archerEnemy;

    public Transform player;

    public int moveDir;

    public ArcherBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.archerEnemy = archerEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().IsDead)
        {
            enemyStateMachine.changeState(archerEnemy.archerMoveState);
        }
    }
    public override void Update()
    {
        base.Update();

        if (archerEnemy.IsPlayerDetected())
        {
            stateTimer = archerEnemy.battleTime;

            if (archerEnemy.IsPlayerDetected().distance < archerEnemy.safeDistance) //当玩家在弓箭手的安全距离之内
            {
                if (CanJump()) //如果可以跳跃
                {
                    enemyStateMachine.changeState(archerEnemy.archerJumpState);
                }

            }

            if (archerEnemy.IsPlayerDetected().distance < archerEnemy.attackDistance)  //角色在敌人攻击范围内
            {
                if (canAttack())  // 设置骷髅怪的攻击间隔时间
                {
                    enemyStateMachine.changeState(archerEnemy.archerAttackState);

                }

            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position, archerEnemy.transform.position) > 7)  // 战斗状态计时器小于0或者角色与敌人距离过远时进入IdleState
            {
                enemyStateMachine.changeState(archerEnemy.archerIdleState);
            }

        }

        BattleStateFlipControl();

        Debug.Log("Skeleton is in battlestate");
    }

    private void BattleStateFlipControl()
    {
        if (player.position.x > archerEnemy.transform.position.x && archerEnemy.facingDir == -1)//弓箭手攻击时不会走向玩家
        {
            archerEnemy.Flip();
        }
        else if (player.position.x < archerEnemy.transform.position.x && archerEnemy.facingDir == 1)
        {
            archerEnemy.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool canAttack()
    {
        if (Time.time >= archerEnemy.enemyLastAttacked + archerEnemy.enemyAttackCoolDown)
        {
            archerEnemy.enemyAttackCoolDown = Random.Range(archerEnemy.enemyMinAttackCoolDown, archerEnemy.enemyMaxAttackCoolDown); //快慢刀
            archerEnemy.enemyLastAttacked = Time.time;
            return true;
        }

        Debug.Log("Attack is on cooldown");
        return false;
    }

    private bool CanJump()
    {
        if (archerEnemy.GroundBehind() == false)
        {
            return false;
        }

        if (archerEnemy.WallBehind() == true)
        {
            return false;
        }

        if (Time.time >= archerEnemy.lastTimeJumped + archerEnemy.jumpCoolDown)
        {
            //准备进入跳跃状态

            archerEnemy.lastTimeJumped = Time.time;
            return true;
        }
        else
        {
            Debug.Log("跳跃冷却中");
            return false;
        }
    }
}
