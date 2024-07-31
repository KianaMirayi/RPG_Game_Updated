using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyGroundedState : EnemyState
{
    public Enemy_Shady shady;

    public Transform player;

    public ShadyGroundedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Shady shady) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.shady = shady;
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

        if (shady.IsPlayerDetected() || Vector2.Distance(shady.transform.position, player.position) < shady.agreDistacne)  // 同方向远距离检测到玩家时，以及在身后短距离检测到玩家时进入战斗状态
        {

            enemyStateMachine.changeState(shady.shadyBattleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }


}
