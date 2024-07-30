using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer : Enemy
{

    [Header("弓箭手信息")]
    [SerializeField] public Vector2 archerJumpVelocity;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private int arrowDamge;

    public float jumpCoolDown;
    public float safeDistance; //弓箭手和玩家的安全距离，小于安全距离则弓箭手跳开
    [HideInInspector] public float lastTimeJumped;

    [Header("Additional collision check")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;
    
    


    public ArcherIdleState archerIdleState { get; private set; }
    public ArcherMoveState archerMoveState { get; private set; }
    public ArcherBattleState archerBattleState { get; private set; }
    public ArcherAttackState archerAttackState { get; private set; }
    public ArcherStunnedState archerStunnedState { get; private set;}
    public ArcherDeadState archerDeadState { get; private set; }

    public ArcherJumpState archerJumpState { get; private set; }




    public override void Awake()
    {
        base.Awake();

        archerIdleState = new ArcherIdleState(this, stateMachine, "Idle", this);

        archerMoveState = new ArcherMoveState(this, stateMachine, "Move", this);

        archerBattleState = new ArcherBattleState(this, stateMachine, "Idle", this);

        archerAttackState = new ArcherAttackState(this, stateMachine, "Attack", this);

        archerStunnedState = new ArcherStunnedState(this, stateMachine, "Stunned", this);

        archerDeadState = new ArcherDeadState(this, stateMachine, "Move", this);

        archerJumpState = new ArcherJumpState(this, stateMachine, "Jump", this);

    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(archerIdleState);
    }

    public override void Update()
    {
        base.Update();
        //stateMachine.enemyCurrentState.Update();

        if (Input.GetKeyDown(KeyCode.U)) //用于测试
        {
            stateMachine.changeState(archerStunnedState);
        }
    }

    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())  //当父类的方法返回了true时
        {
            stateMachine.changeState(archerStunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(archerDeadState);
    }

    public override void AnimationSpecialAttackTrigger()
    {
        Debug.Log("射箭");

        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, Quaternion.identity);

        newArrow.GetComponent<Arrow_Controller>().SetupArrow(arrowSpeed * facingDir, stats);
        
    }


    //用于防止弓箭手向后跳时跳出当前地形,当检测不到地面时，将不在向后跳
    public bool GroundBehind() => Physics2D.BoxCast(groundBehindCheck.position, groundBehindCheckSize, 0, Vector2.zero, 0, whatIsGround);
    public bool WallBehind() => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDir, wallCheckDistance + 2, whatIsGround);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireCube(groundBehindCheck.position, groundBehindCheckSize);
    }

}