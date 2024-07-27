using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Skeleton : Enemy
{
    public SkeletonIdleState skeIdleState { get; private set; }

    public SkeletonMoveState skeletonMoveState { get; private set; }

    public SkeletonBattleState skeletonBattleState { get; private set; }

    public SkeletonAttackState skeletonAttackState { get; private set; }

    public SkeletonStunnedState skeletonStunnedState { get; private set; }

    public SkeletonDeadState skeletonDeadState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        skeIdleState = new SkeletonIdleState(this, stateMachine, "Idle", this);

        skeletonMoveState = new SkeletonMoveState(this, stateMachine, "Move", this);

        skeletonBattleState = new SkeletonBattleState(this, stateMachine, "Move", this);// ս��״̬ʱ�ƶ�����moveState�Ķ���

        skeletonAttackState = new SkeletonAttackState(this, stateMachine, "Attack", this);

        skeletonStunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);

        skeletonDeadState = new SkeletonDeadState(this, stateMachine, "Move", this);

    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(skeIdleState);
    }

    public override void Update()
    {
        base.Update();
        //stateMachine.enemyCurrentState.Update();

        if (Input.GetKeyDown(KeyCode.U)) //���ڲ���
        {
            stateMachine.changeState(skeletonStunnedState);
        }
    }

    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())  //������ķ���������trueʱ
        {
            stateMachine.changeState(skeletonStunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(skeletonDeadState);
    }
}
