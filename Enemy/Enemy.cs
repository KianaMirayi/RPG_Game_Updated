using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{

    public LayerMask whatIsPlayer;

    [Header("Move Info")]
    public float moveSpeed;
    public float defalutMoveSpeed;

    public float idleTime;
    public float battleTime;

    [Header("Attack info")]
    public float attackDistance;
    public float enemyAttackCoolDown;
    public float enemyLastAttacked;

    [Header("Stunned info")]
    public float StunnedDuration;  //������ʾ���ܵ���Ч��ʱ�ĳ���ʱ��
    public Vector2 StunDir;  //������ʾ���ܵ���Ч��ʱ���˶�����
    public bool CanBeStunned;  //�����жϵ����Ƿ��ܱ�����
    [SerializeField] public GameObject CouneterImage;  //����ָʾ���˷���������ʱ�㣬������ɫ���е�����ʱ��



    public EnemyStateMachine stateMachine { get; private set; }

    public String LastAnimBoolName { get; private set; }

    public override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();

        defalutMoveSpeed = moveSpeed;
    }

    public override void Start()
    {
        base.Start();
        
    }

    public override void Update()
    {
        base.Update();

        stateMachine.enemyCurrentState.Update();

    }

    public virtual void AssignLastAnimName(string _animBoolName)
    { 
        LastAnimBoolName = _animBoolName;
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 7, whatIsPlayer);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + attackDistance * facingDir, wallCheck.position.y));
    }

    public virtual void AnimationTrigger()
    {
        stateMachine.enemyCurrentState.AnimationFinishTrigger();
    }


    public virtual void OpenCounterWindow()  //��ɫ�������
    { 
        CanBeStunned = true;
        CouneterImage.SetActive(true);
    }

    public virtual void CloseCounterWindow() //��ɫ������ʧ
    {
        CanBeStunned = false;
        CouneterImage.SetActive(false);
    }

    public virtual bool CanbeStunned()  //�ж��Ƿ�����Ƿ�����ܵ������˺�
    {
        if (CanBeStunned)
        { 
            CloseCounterWindow();
            return true;
        }

        return false;
    } 

    public void FreezeEnemy(bool _freeze)  //��ɫʩ�Ŵ���ʱʹ���˶���
    {
        if (_freeze)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defalutMoveSpeed;
            anim.speed = 1;

        }
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {

        moveSpeed = moveSpeed * (1 - _slowPercentage);

        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDafaultSpeed", _slowDuration);
        
    }

    public override void ReturnDafaultSpeed()
    {
        base.ReturnDafaultSpeed();

        moveSpeed = defalutMoveSpeed;

        anim.speed = 1;
    }

}
