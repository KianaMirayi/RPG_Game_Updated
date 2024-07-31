using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shady : Enemy
{

    [Header("阴暗的家伙")]
    public float battleStateMoveSpeed;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxSize;




    public ShadyIdleState shadyIdleState { get; private set; }
    public ShadyMoveState shadyMoveState { get; private set; }
    public ShadyBattleState shadyBattleState { get; private set; }
    public ShadyStunnedState shadyStunnedState { get; private set; }
    public ShadyDeadState shadyDeadState { get; private set; }



    public override void Awake()
    {
        base.Awake();

        shadyIdleState = new ShadyIdleState(this, stateMachine, "Idle", this);

        shadyMoveState = new ShadyMoveState(this, stateMachine, "Move", this);

        shadyBattleState = new ShadyBattleState(this, stateMachine, "MoveFast", this);

        shadyStunnedState = new ShadyStunnedState(this, stateMachine, "Stunned", this);

        shadyDeadState = new ShadyDeadState(this, stateMachine, "Dead", this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(shadyIdleState);
    }

    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())  //当父类的方法返回了true时
        {
            stateMachine.changeState(shadyStunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(shadyDeadState);
    }

    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newExplosion = Instantiate(explosionPrefab, attackCheck.position, Quaternion.identity);
        newExplosion.GetComponent<ShadyExplosiveController>().SetupExplosion(stats,growSpeed,maxSize,attackCheckRadius);

        cd.enabled = false;
        rb.gravityScale = 0;
    }

    public void ShadySelfDestory()
    {
        Destroy(gameObject);
    }
}
