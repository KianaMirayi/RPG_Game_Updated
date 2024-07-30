using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SlimeType
{ 

    big,
    medium,
    small
}

public class Enemy_Slime : Enemy
{
    [Header("史莱姆体型")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimesToCreate;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreationVelocity;
    [SerializeField] private Vector2 maxCreationVelocity;




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

        if (slimeType == SlimeType.small)
        { 
            return;
        }

        CreateSlime(slimesToCreate, slimePrefab);

    }

    private void CreateSlime(int _amountOfSlime, GameObject _slimePrefab)
    {
        for (int i = 0; i < _amountOfSlime; i++)
        {
            
            

            GameObject newSlime = Instantiate(_slimePrefab,new Vector2(transform.position.x ,transform.position.y + 1), Quaternion.identity);
            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    private void SetupSlime(int _facingDir)
    {
        if (_facingDir != facingDir)
        {
            Flip();
        }

        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
        float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

        IsKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * facingDir, yVelocity);

        Invoke("CancelKnockBack", 1.5f);
    }

    private void CancelKnockBack()
    { 
        IsKnocked = false;
    }
}
