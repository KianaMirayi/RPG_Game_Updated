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

    public Vector2[] AttackMovement;  //声明了一个数组
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

    //[Header("Collision info")]  //创建检测地面和墙壁的所需的变量
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

        if (Time.timeScale == 0) //暂停游戏
        {
            return;
        }

        stateMachine.currentState.Update();

        CheckDashInput();

        //Debug.Log(IsWallDetected());

        if (Input.GetKeyDown(KeyCode.E) && SkillManager.SkillInstance.Crystal.crystalUnlocked)  //当水晶技能解锁时，按下E可以召唤水晶
        {
            SkillManager.SkillInstance.Crystal.CanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))  //按1使用药水
        {
            Debug.Log("Use Flask");
            Inventory.Instance.UseFlask();
        }

    }

    //public void setVelocity(float xVelocity, float yVelocity)
    //{
    //    rb.xVelocity = new Vector2(xVelocity, yVelocity);
    //    FlipController(xVelocity);  // 在这里将角色在横轴方向的向量输入作为参数传递给FlipController，在进行判断之后调用相应的翻转函数
    //}

    //public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);  //当在whatIsGround图层检测到物体时（即地面），IsGroundDetected的返回值为true,否则为false

    //public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);//wallCheckDistance * facingDir 是为了在角色调转方向时也能对应检测墙壁
    //// Physics2D.Raycast 方法
    //// Physics2D.Raycast 是 Unity 的 2D 物理引擎中的一个方法，用于发射一条射线并检测它是否与任何碰撞体（collider）相交。这个方法非常常用来检测物体是否在某个范围内，或者是否有障碍物阻挡。
    //// public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
    ////    参数
    //                                //    origin：射线的起点坐标（Vector2 类型）。
    //                                //    direction：射线的方向（Vector2 类型）。
    //                                //    distance（可选）：射线的最大检测距离（默认为 Mathf.Infinity，即无限远）。
    //                                //    layerMask（可选）：层掩码，用于指定哪些层的物体会与射线相交（默认为所有层）。

    //public void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    //}
    //在你的代码中，new Vector3被用来定义射线检测的终点位置。具体来说：
    //Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    //这个代码段在OnDrawGizmos方法中，用于在Unity编辑器中绘制一条线，以可视化射线检测的范围。让我们详细解释一下：

    //groundCheck.position：起点位置，通常是一个位于角色脚下的Transform组件的位置。
    //new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance)：
    //groundCheck.position.x：线段终点的x坐标与起点的x坐标相同。
    //groundCheck.position.y - groundCheckDistance：线段终点的y坐标位于起点的y坐标下方groundCheckDistance的距离。
    //groundCheck.position.z：因为没有显式指定z坐标，z将默认为0（对于2D游戏通常是这样）。
    //这个向量定义了从groundCheck位置向下groundCheckDistance距离的位置。

    //Gizmos.DrawLine 方法
    //Gizmos.DrawLine 是 Unity 编辑器中的一个方法，用于绘制一条线段。在编辑器模式下，它帮助开发者可视化场景中的某些数据，如物理检测范围、路径等。

    //public static void DrawLine(Vector3 from, Vector3 to);
    //    from：线段的起点（Vector3 类型）。 Vector3 from本身就是一个坐标
    //    to：线段的终点（Vector3 类型）。   Vector3 to  本身就是一个坐标

    //public void Flip()
    //{
    //    facingDir = facingDir * -1;  // 每次调用翻转函数时，将facingDir的值乘以-1
    //    facingRight = !facingRight;  // 每次调用翻转函数时，将facingRight的值反转
    //    rb.transform.Rotate(0, 180, 0);  // transform.Rotate是一个方法，可以对角色相应的变换
    //}

    //public void FlipController(float x)  //创建一个float型参数以接受角色在横轴方向的向量输入
    //{
    //    if (x < 0 && facingRight)  // 当角色准备向左移动，而面朝右侧时，进行翻转
    //    {
    //        Flip();
    //    }

    //    if (x > 0 && !facingRight)  // 当角色准备向右移动，而面朝左侧时，进行翻转
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


        if (skillManager.Dash.dashUnlocked == false)  //当冲刺未被解锁时，将不能使用冲刺
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
                dashDir = facingDir;  //解决当玩家不选择方向而按下冲刺键时角色的冲刺方向，将其设定为角色的朝向
            }
            stateMachine.changeState(dashState);
        }
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();   // 这么写是为了明确地指向这个函数，在Player脚本中没有声明PlayerState类的成员
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(DeadState);
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)  //受元素伤害攻击移动速度减慢
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


