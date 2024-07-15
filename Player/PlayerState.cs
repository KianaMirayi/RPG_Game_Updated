using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    public Player player;
    protected PlayerStateMachine stateMachine;
    private string animBoolName;


    public float xInput;  // �����������Խ�����ҵ�����
    public float yInput;

    protected bool TriggerCalled;

    
    public Rigidbody2D rb;

    


    public float stateTimer;  // �����趨״̬����ʱ��
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        rb = player.rb;

        player.anim.SetBool(animBoolName, true);

        TriggerCalled = false;

        //Debug.Log("I enter" + animBoolName);

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;


        xInput = Input.GetAxisRaw("Horizontal");  //������ں����ϵķ������봫�ݸ�xInput
        yInput = Input.GetAxisRaw("Vertical");
        player.setVelocity(xInput * player.moveSpeed, rb.velocity.y);
        player.anim.SetFloat("yVelocity", rb.velocity.y);  // ������������ϵ������仯�����Animator�д�����yVelocity

        //Debug.Log("I am in" + animBoolName);
    }
    
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

        //Debug.Log("I exit" + animBoolName);
    }

    public virtual void AnimationFinishTrigger()
    {
        TriggerCalled = true;
    }



}
