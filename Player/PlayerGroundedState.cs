using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        Debug.Log("ground detected");


        if (Input.GetKeyDown(KeyCode.R))  //���кڶ�
        {

            stateMachine.changeState(player.BlackHoleState);
            SkillManager.SkillInstance.BlackHole.CanGrow = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())  //����������Ҽ�������û�н�ʱ
        {
            stateMachine.changeState(player.AimSwordState);
        }

        if (Input.GetKeyDown(KeyCode.Q)) //�񵲷�������
        {
            stateMachine.changeState(player.counterAttackState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.changeState(player.attackState);
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.changeState(player.airState);

        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.changeState(player.jumpState);
        }
    }


    public bool HasNoSword()
    {
        if (!player.Sword)
        {
            return true;
        }

        player.Sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
