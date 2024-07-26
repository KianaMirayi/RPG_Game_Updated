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

        stateTimer = player.CounterAttackDuration;  // ���ý�ɫ�мܺ͵����ĳ���ʱ��

        player.anim.SetBool("SuccessfullyCounterAttack", false);  //���жϳɹ������Ķ���bool����Ϊfalse
    }

    public override void Update()
    {
        base.Update();

        player.rb.velocity = new Vector2(0, 0);  // ��ɫ�ڽ��е���ʱ�����Խ����ƶ�

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);  // ��һ����Χ�ڼ�����к���ײ��������

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)  //��⵽����
            {
                if (hit.GetComponent<Enemy>().CanbeStunned()) // �������˿����ܵ������˺�
                {
                    stateTimer = 1;
                    AudioManager.instance.PlaySfx(40, player.transform); //���ŵ����ɹ���Ч

                    player.anim.SetBool("SuccessfullyCounterAttack", true);  //���ųɹ������Ķ���

                    player.skillManager.Parry.UseSkill();//�����ɹ�ʱ�ظ�����ֵ


                    if (CanCreateClone)
                    { 
                        CanCreateClone =false;
                        //player.skillManager.Clone.CreateCloneWithDelay(hit.transform);  //�ɹ������������Ӱ��ʹ���˻���
                        player.skillManager.Parry.CreateMirageOnParry(hit.transform);
                        
                    }

                    PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
                    EnemyStats  enemyStats = hit.GetComponent<EnemyStats>();
                    playerStats.DoDamage(enemyStats);

                }
            }
        }

        if (stateTimer < 0 || TriggerCalled)  //���мܺ͵�������ʱ��С��0���߶�������
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
