using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class Player : Entity
{


    [Header("Attack Detail")]

    public Vector2[] AttackMovement;  //������һ������
    [SerializeField]public float CounterAttackDuration;

    [Header("Move info")]
    public float moveSpeed = 12;
    public float jumpforce = 10;
    public float SwordReturnImpact;
    public float DefaultMoveSpeed;
    public float DefaultJumpForce;


    [Header("Dash info")]
    //[SerializeField] public float dashCoolDown;
    //public float dashUsetime;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir;
    public float DefaultDashSpeed;

    //[Header("Collision info")]  //�����������ǽ�ڵ�����ı���
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckDistance;
    //[SerializeField] private Transform wallCheck;
    //[SerializeField] private float wallCheckDistance;
    //[SerializeField] private LayerMask whatIsGround;


    //public float facingDir { get; private set; } = 1;
    //public bool facingRight = true;

    //public Animator anim;
    //public Rigidbody2D rb;
    
    public SkillManager skillManager;

    public GameObject Sword;

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerWallJumpState wallJumpState { get; private set; }

    public PlayerPrimaryAttackState attackState { get; private set; }

    public PlayerCounterAttackState counterAttackState { get; private set; }

    public PlayerAimSwordState AimSwordState { get; private set; }
    
    public PlayerCatchSwordState CatchSwordState { get; private set; }

    public PlayerBlackHoleState BlackHoleState { get; private set; }

    public PlayerDeadState DeadState { get; private set; }

    #endregion

    public override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");

        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState  =  new PlayerAirState(this, stateMachine, "Jump");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");

        attackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");

        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        AimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");

        CatchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");

        BlackHoleState = new PlayerBlackHoleState(this, stateMachine,"Jump");

        DeadState = new PlayerDeadState(this, stateMachine, "Die");

    }

    public override void Start()
    {
        base.Start();

        //anim = GetComponentInChildren<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        skillManager = SkillManager.SkillInstance;
        stateMachine.Initialize(idleState);

        DefaultMoveSpeed = moveSpeed;
        DefaultJumpForce = jumpforce;
        DefaultDashSpeed = dashSpeed;
    }

    public override void Update()
    {
        base.Update();

        if (Time.timeScale == 0) //��ͣ��Ϸ
        {
            return;
        }

        stateMachine.currentState.Update();

        CheckDashInput();

        //Debug.Log(IsWallDetected());

        if (Input.GetKeyDown(KeyCode.E) && SkillManager.SkillInstance.Crystal.crystalUnlocked)  //��ˮ�����ܽ���ʱ������E�����ٻ�ˮ��
        {
            SkillManager.SkillInstance.Crystal.CanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))  //��1ʹ��ҩˮ
        {
            Debug.Log("Use Flask");
            Inventory.Instance.UseFlask();
        }

    }

    //public void setVelocity(float xVelocity, float yVelocity)
    //{
    //    rb.xVelocity = new Vector2(xVelocity, yVelocity);
    //    FlipController(xVelocity);  // �����ｫ��ɫ�ں��᷽�������������Ϊ�������ݸ�FlipController���ڽ����ж�֮�������Ӧ�ķ�ת����
    //}

    //public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);  //����whatIsGroundͼ���⵽����ʱ�������棩��IsGroundDetected�ķ���ֵΪtrue,����Ϊfalse

    //public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);//wallCheckDistance * facingDir ��Ϊ���ڽ�ɫ��ת����ʱҲ�ܶ�Ӧ���ǽ��
    //// Physics2D.Raycast ����
    //// Physics2D.Raycast �� Unity �� 2D ���������е�һ�����������ڷ���һ�����߲�������Ƿ����κ���ײ�壨collider���ཻ����������ǳ���������������Ƿ���ĳ����Χ�ڣ������Ƿ����ϰ����赲��
    //// public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
    ////    ����
    //                                //    origin�����ߵ�������꣨Vector2 ���ͣ���
    //                                //    direction�����ߵķ���Vector2 ���ͣ���
    //                                //    distance����ѡ�������ߵ��������루Ĭ��Ϊ Mathf.Infinity��������Զ����
    //                                //    layerMask����ѡ���������룬����ָ����Щ���������������ཻ��Ĭ��Ϊ���в㣩��

    //public void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    //}
    //����Ĵ����У�new Vector3�������������߼����յ�λ�á�������˵��
    //Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    //����������OnDrawGizmos�����У�������Unity�༭���л���һ���ߣ��Կ��ӻ����߼��ķ�Χ����������ϸ����һ�£�

    //groundCheck.position�����λ�ã�ͨ����һ��λ�ڽ�ɫ���µ�Transform�����λ�á�
    //new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance)��
    //groundCheck.position.x���߶��յ��x����������x������ͬ��
    //groundCheck.position.y - groundCheckDistance���߶��յ��y����λ������y�����·�groundCheckDistance�ľ��롣
    //groundCheck.position.z����Ϊû����ʽָ��z���꣬z��Ĭ��Ϊ0������2D��Ϸͨ������������
    //������������˴�groundCheckλ������groundCheckDistance�����λ�á�

    //Gizmos.DrawLine ����
    //Gizmos.DrawLine �� Unity �༭���е�һ�����������ڻ���һ���߶Ρ��ڱ༭��ģʽ�£������������߿��ӻ������е�ĳЩ���ݣ��������ⷶΧ��·���ȡ�

    //public static void DrawLine(Vector3 from, Vector3 to);
    //    from���߶ε���㣨Vector3 ���ͣ��� Vector3 from�������һ������
    //    to���߶ε��յ㣨Vector3 ���ͣ���   Vector3 to  �������һ������

    //public void Flip()
    //{
    //    facingDir = facingDir * -1;  // ÿ�ε��÷�ת����ʱ����facingDir��ֵ����-1
    //    facingRight = !facingRight;  // ÿ�ε��÷�ת����ʱ����facingRight��ֵ��ת
    //    rb.transform.Rotate(0, 180, 0);  // transform.Rotate��һ�����������ԶԽ�ɫ��Ӧ�ı任
    //}

    //public void FlipController(float x)  //����һ��float�Ͳ����Խ��ܽ�ɫ�ں��᷽�����������
    //{
    //    if (x < 0 && facingRight)  // ����ɫ׼�������ƶ������泯�Ҳ�ʱ�����з�ת
    //    {
    //        Flip();
    //    }

    //    if (x > 0 && !facingRight)  // ����ɫ׼�������ƶ������泯���ʱ�����з�ת
    //    {
    //        Flip();
    //    }
    //}





    public void AssignNewSword(GameObject _newSword)
    { 
        Sword = _newSword;
    }

    public void CatchTheSword()
    {
        stateMachine.changeState(CatchSwordState);
        Destroy(Sword);
    }

    public void ExitBlackHoleSkill()
    {
        stateMachine.changeState(airState);  
    }

    public void CheckDashInput()
    {
        //if (IsWallDetected())
        //{ 
        //    return;
        //}
        //dashUsetime -= Time.deltaTime;


        if (skillManager.Dash.dashUnlocked == false)  //�����δ������ʱ��������ʹ�ó��
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) /*&& dashUsetime < 0*/ && skillManager.Dash.CanUseSkill())
        {
            AudioManager.instance.PlaySfx(38, transform);
            fx.PlayDashFX();
            //dashUsetime = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            {
                dashDir = facingDir;  //�������Ҳ�ѡ��������³�̼�ʱ��ɫ�ĳ�̷��򣬽����趨Ϊ��ɫ�ĳ���
            }
            stateMachine.changeState(dashState);
        }
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();   // ��ôд��Ϊ����ȷ��ָ�������������Player�ű���û������PlayerState��ĳ�Ա
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(DeadState);
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)  //��Ԫ���˺������ƶ��ٶȼ���
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpforce = jumpforce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);

        anim.speed = anim.speed * (1 - _slowPercentage);


        Invoke("ReturnDafaultSpeed", _slowDuration);
    }

    public override void ReturnDafaultSpeed()
    {
        base.ReturnDafaultSpeed();

        moveSpeed = DefaultMoveSpeed;
        jumpforce = DefaultJumpForce;
        dashSpeed = DefaultDashSpeed;

    }

    protected override void SetUpZeroKnockPower()
    {
        KnockBackPower = new Vector2(0, 0);
    }

}


