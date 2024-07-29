using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{

    public SlimeIdleState slimeIdleState { get; private set; }

    public SlimeMoveState slimeMoveState { get; private set; }

    public SlimeBattleState slimeBattleState { get; private set; }

    public SlimeAttackState slimeAttackState { get; private set; }

    public SlimeStunState slimeStunnedState { get; private set; }

    public SlimeDeadState slimeDeadState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        slimeIdleState = new SlimeIdleState(this, stateMachine, "Idle", this);

        slimeMoveState = new SlimeMoveState(this, stateMachine, "Move", this);

        slimeBattleState = new SlimeBattleState(this, stateMachine, "Move", this);// 战斗状态时移动调用moveState的动画

        slimeAttackState = new SlimeAttackState(this, stateMachine, "Attack", this);

        slimeStunnedState = new SlimeStunState(this, stateMachine, "Stunned", this);

        slimeDeadState = new SlimeDeadState(this, stateMachine, "Idle", this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(slimeIdleState);
    }

    public override void Update()
    {
        base.Update();
    }

    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())  //当父类的方法返回了true时
        {
            stateMachine.changeState(slimeStunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(slimeDeadState);
    }
}
