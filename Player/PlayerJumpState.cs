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

        if (rb.velocity.y != 0)  // ����ɫ��ʼ�������һ�̣�y����Ϊ��ֵ������⵽y����Ϊ��ֵʱ����״̬ת��Ϊairstate������ɫ���ڿ���
        {
            stateMachine.changeState(player.airState);
        }
    }
}
