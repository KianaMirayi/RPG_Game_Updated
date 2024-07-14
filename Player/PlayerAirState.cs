using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsGroundDetected())
        {
            //Debug.Log("IsGroundDetected");
            stateMachine.changeState(player.idleState);
        }

        if (player.IsWallDetected())
        {
            stateMachine.changeState(player.wallSlideState);
        }

        if (xInput != 0)
        {
            player.setVelocity(player.moveSpeed * 0.8f * xInput, player.rb.velocity.y);  // ����ɫ�ڿ���ʱ���ƶ��ٶ�Ϊ�ڵ����80%������Ҳ���Ա�֤��ɫ�����ڿ����ƶ�
        }

    }
}
