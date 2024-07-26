using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool CanCreateClone;

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanCreateClone = true;

        stateTimer = player.CounterAttackDuration;  // 设置角色招架和弹反的持续时间

        player.anim.SetBool("SuccessfullyCounterAttack", false);  //将判断成功弹反的动画bool设置为false
    }

    public override void Update()
    {
        base.Update();

        player.rb.velocity = new Vector2(0, 0);  // 角色在进行弹反时不可以进行移动

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);  // 在一定范围内检测所有含碰撞体积的组件

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)  //检测到敌人
            {
                if (hit.GetComponent<Enemy>().CanbeStunned()) // 当检测敌人可以受到弹反伤害
                {
                    stateTimer = 1;
                    AudioManager.instance.PlaySfx(40, player.transform); //播放弹反成功音效

                    player.anim.SetBool("SuccessfullyCounterAttack", true);  //播放成功弹反的动画

                    player.skillManager.Parry.UseSkill();//弹反成功时回复生命值


                    if (CanCreateClone)
                    { 
                        CanCreateClone =false;
                        //player.skillManager.Clone.CreateCloneWithDelay(hit.transform);  //成功弹反将创造幻影并使敌人击退
                        player.skillManager.Parry.CreateMirageOnParry(hit.transform);
                        
                    }

                    PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
                    EnemyStats  enemyStats = hit.GetComponent<EnemyStats>();
                    playerStats.DoDamage(enemyStats);

                }
            }
        }

        if (stateTimer < 0 || TriggerCalled)  //当招架和弹反持续时间小于0或者动画结束
        {
            stateMachine.changeState(player.idleState);
            //Debug.Log("0000000"+TriggerCalled);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
