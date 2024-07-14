using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    public float FlyTime = 0.4f;
    public bool SkillUsed;

    public float defaultGravity;

    public PlayerBlackHoleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        defaultGravity = rb.gravityScale;

        SkillUsed = false;
        stateTimer = FlyTime;
        rb.gravityScale = 0;
        //player.MakeTransparent(true);

    }
    // 在所有黑洞攻击结束后，在BlackHole_Skill_Controller脚本中退出黑洞技能
    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 12);
            
            //player.sr.color = new Color(1, 1, 1, player.sr.color.a - (Time.deltaTime));
        }
        if (stateTimer < 0)
        { 
            rb.velocity = new Vector2(0, -0.1f);
            

            if (!SkillUsed)
            {
                if (player.skillManager.BlackHole.CanUseSkill())
                {
                    SkillUsed = true;

                }
            }

        }

        if(player.skillManager.BlackHole.BlackHoleSkillCompleted())
        {
            stateMachine.changeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = defaultGravity;
        player.MakeTransparent(false);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
