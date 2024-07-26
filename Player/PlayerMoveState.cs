using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySfx(14, null); //�Ų���
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSfx(14);

    }

    public override void Update()
    {
        base.Update();
        if (xInput == 0)
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
