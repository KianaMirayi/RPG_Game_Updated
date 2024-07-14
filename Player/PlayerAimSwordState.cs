using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{

    
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skillManager.Sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        



    }

    public override void Update()
    {
        base.Update();

       
        player.ZeroVelocity();  //玩家在瞄准时不可移动

        if (Input.GetKeyUp(KeyCode.Mouse1))  //松开鼠标右键回到闲置状态
        {
            stateMachine.changeState(player.idleState);
        }

        //瞄准时根据鼠标与角色的相对位置进行翻转
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
        {
            player.Flip();
        }
        else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
        { 
            player.Flip();
        }

    }

    
}
