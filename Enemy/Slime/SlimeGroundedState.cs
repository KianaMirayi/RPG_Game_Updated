using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundedState : EnemyState
{
    public Enemy_Slime slimeEnemy;

    public Transform player;
    public SlimeGroundedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime _slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.slimeEnemy = _slimeEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        if (slimeEnemy.IsPlayerDetected() || Vector2.Distance(slimeEnemy.transform.position, player.position) < 2)  // 同方向远距离检测到玩家时，以及在身后短距离检测到玩家时进入战斗状态
        {

            enemyStateMachine.changeState(slimeEnemy.slimeBattleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
