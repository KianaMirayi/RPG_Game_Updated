using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    public Player player;
    protected PlayerStateMachine stateMachine;
    private string animBoolName;


    public float xInput;  // 声明浮点数以接受玩家的输入
    public float yInput;

    protected bool TriggerCalled;

    
    public Rigidbody2D rb;

    


    public float stateTimer;  // 用于设定状态持续时间
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


        xInput = Input.GetAxisRaw("Horizontal");  //将玩家在横轴上的方向输入传递给xInput
        yInput = Input.GetAxisRaw("Vertical");
        player.setVelocity(xInput * player.moveSpeed, rb.velocity.y);
        player.anim.SetFloat("yVelocity", rb.velocity.y);  // 将玩家在纵轴上的向量变化传输给Animator中创建的yVelocity

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
