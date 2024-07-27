using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public bool dash;
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();



        dash = false;

        stateTimer = player.dashDuration;

        //SkillManager.SkillInstance.Clone.CreateClone(player.transform);
        //player.skillManager.Clone.CreateClone(player.transform,Vector2.zero);
        //player.skillManager.Clone.CreateCloneOnDashBegin();
        player.skillManager.Dash.CreateCloneOnDash();

        player.stats.MakeInvincible(true); //冲刺时玩家无敌
    }

    public override void Exit()
    {
        base.Exit();

        //player.skillManager.Clone.CreateCloneOnDashOver();
        player.skillManager.Dash.CreateCloneOnArrival();
        player.setVelocity(0, rb.velocity.y);

        player.stats.MakeInvincible(false); // 退出冲刺时无敌失效
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log("dash");

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    enemyStateMachine.changeState(player.attackState);
        //}

        if (player.IsWallDetected())
        {
            stateMachine.changeState(player.wallSlideState);
        }

        player.setVelocity(player.dashSpeed * player.dashDir, rb.velocity.y);
        dash = true;

        Debug.Log("the value of dash " + dash);

        if (stateTimer < 0)
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
 