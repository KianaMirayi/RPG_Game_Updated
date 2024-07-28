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
        //Debug.Log("ground detected");


        if (Input.GetKeyDown(KeyCode.R) && player.skillManager.BlackHole.BlackHoleUnlocked)  //大招黑洞
        {
            if (player.skillManager.BlackHole.CoolDownTimer > 0)
            {
                player.fx.CreatePopUpText("技能冷却中");
                return;
            }

            stateMachine.changeState(player.BlackHoleState);
            SkillManager.SkillInstance.BlackHole.CanGrow = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.skillManager.Sword.RegularSwordUnlocked)  //当按下鼠标右键，并且没有剑时
        {
            stateMachine.changeState(player.AimSwordState);
        }

        if (Input.GetKeyDown(KeyCode.Q) && player.skillManager.Parry.parryUnlocked && player.skillManager.Parry.CoolDownTimer <= 0) //格挡反击技能,当解锁该技能时才可使用
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
        Debug.Log("55555");
        return false;
    }
}
