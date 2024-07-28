using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {

        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpforce);
    }

    public override void Exit()
    {
        base.Exit();

        //player.fx.PlayDustFX();
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y != 0)  // 当角色开始下落的那一刻，y向量为负值，当检测到y向量为负值时，将状态转变为airstate，即角色正在空中
        {
            stateMachine.changeState(player.airState);
        }
    }
}
