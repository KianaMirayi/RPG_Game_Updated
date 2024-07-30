using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{

    public LayerMask whatIsPlayer;

    [Header("Move Info")]
    public float moveSpeed;
    public float defalutMoveSpeed;
    public float defaultAnimSpeed;

    public float idleTime;
    public float battleTime;

    [Header("Attack info")]
    public float agreDistacne = 2;
    public float attackDistance;
    public float enemyAttackCoolDown;
    public float enemyMaxAttackCoolDown;
    public float enemyMinAttackCoolDown;
    public float enemyLastAttacked;

    [Header("Stunned info")]
    public float StunnedDuration;  //������ʾ���ܵ���Ч��ʱ�ĳ���ʱ��
    public Vector2 StunDir;  //������ʾ���ܵ���Ч��ʱ���˶�����
    public bool CanBeStunned;  //�����жϵ����Ƿ��ܱ�����
    [SerializeField] public GameObject CouneterImage;  //����ָʾ���˷���������ʱ�㣬������ɫ���е�����ʱ��



    public EnemyStateMachine stateMachine { get; private set; }

    public String LastAnimBoolName { get; private set; }

    //public EntityFX fx;

    public override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();

        defalutMoveSpeed = moveSpeed;
        
    }

    public override void Start()
    {
        base.Start();
        //fx = GetComponent<EntityFX>();


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

    public virtual void AnimationFinishTrigger()
    {
        stateMachine.enemyCurrentState.AnimationFinishTrigger();
    }

    public virtual void AnimationSpecialAttackTrigger() //for Archer
    { 
        
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

    

    public void FreezeEnemyWhenVitalHitFromEnemy()  //�����Ѫ����ʱʹ�ܱߵĵ��˶���һ��ʱ�䣬��FreezeEnemy_Effect��������Э��
    {
        moveSpeed = 0;
        anim.speed = 0;

        Invoke("ReturnDefault", 2);
    }
    public void ReturnDefault()  //ʹ���˶����ƶ��ٶȵȻָ�Ĭ��ֵ����FreezeEnemyWhenVitalHitFromEnemy��ʹ��
    {
        moveSpeed = defalutMoveSpeed;
        anim.speed = 1;
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


    public void SLowEnemy()
    {
        moveSpeed = defalutMoveSpeed * 0.8f;
        anim.speed = anim.speed * 0.8f;

        Invoke("ReturnDefault", 2);

    }
}
