using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    public Transform Sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Sword = player.Sword.transform;

        //player.fx.PlayDustFX(); //抓住剑时特效
        player.fx.ScreenShake(player.fx.swordShakeImpact);


        if (player.transform.position.x > Sword.position.x && player.facingDir == 1)
        {
            player.Flip();
        }
        else if (player.transform.position.x < Sword.position.x && player.facingDir == -1)
        {
            player.Flip();
        }

        rb.velocity = new Vector2(player.SwordReturnImpact * -player.facingDir, rb.velocity.y);

    }


    public override void Update()
    {
        base.Update();

        if (TriggerCalled)
        {
            stateMachine.changeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
