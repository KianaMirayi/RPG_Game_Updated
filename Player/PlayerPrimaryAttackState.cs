using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    public int comboCounter;
    public float lastTimeAttacked;
    public int comboableWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //var dashState = player.dashState as PlayerDashState;

        if (comboCounter > 2 || Time.time > lastTimeAttacked + comboableWindow)
        {
            comboCounter = 0;
        }

        Debug.Log(comboCounter);

        player.anim.SetInteger("comboCounter", comboCounter);

        //rb.velocity = new Vector2(player.AttackMovement[comboCounter].x * player.facingDir, player.AttackMovement[comboCounter].y);
        player.setVelocity(player.AttackMovement[comboCounter].x * player.facingDir, player.AttackMovement[comboCounter].y);

        stateTimer = 0.1f;

    }

    public override void Exit()
    {
        base.Exit();

        comboCounter ++;
        lastTimeAttacked = Time.time;
        Debug.Log(lastTimeAttacked);
    }

    public override void Update()
    {
        base.Update();

        if (TriggerCalled)  // 此时TriggerCalled的值为true
        {
            stateMachine.changeState(player.idleState);
            Debug.Log("The value of TriggerCalled "+TriggerCalled);
        }

        if (stateTimer < 0)  // 设置stateTimer的目的时让角色停下来进行攻击时有顿挫感
        { 
            player.setVelocity(0,0);  // 在攻击时角色的移动速度  
        }
        // 或者 rb.velocity = new Vector2(0, rb.velocity.y);

        //if (Input.GetKeyDown(KeyCode.Mouse0) && player.dashState.dash && !player.IsGroundDetected())
        //{
            
        //    player.setVelocity(rb.velocity.x, rb.velocity.y);
        //}
    }
}
