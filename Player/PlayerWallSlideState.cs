using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() == false) //当玩家在滑墙中没有检测到墙壁时，则进入滞空状态
        {
            stateMachine.changeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space))  //当检测到玩家按下空格键时，进入跳墙状态
        {
            stateMachine.changeState(player.wallJumpState);
        }

        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else 
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f); 
        }


        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.changeState(player.idleState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.changeState(player.idleState);
        }

        if (!player.IsGroundDetected() && !player.IsWallDetected())
        {
            stateMachine.changeState(player.airState);
        }


    }
}
