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

       
        player.ZeroVelocity();  //�������׼ʱ�����ƶ�

        if (Input.GetKeyUp(KeyCode.Mouse1))  //�ɿ�����Ҽ��ص�����״̬
        {
            stateMachine.changeState(player.idleState);
        }

        //��׼ʱ����������ɫ�����λ�ý��з�ת
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
