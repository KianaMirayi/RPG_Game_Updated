using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{
    public CapsuleCollider2D cd;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;
    public EntityFX fx;
    public CharacterStats stats;

    [Header("Collsion info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("KnockBack info")]
    [SerializeField] protected Vector2 KnockBackPower;  //用于表示被击退时的位置向量
    [SerializeField] protected float KnockBcakDuration;  //用于表示在被击退状态下的时间
    public bool IsKnocked;  // 用于判断是否被击退



    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int KnockBackDir { get; private set; }

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipped;  //委托
    public virtual void Awake()
    { 
    
    }
    public virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        cd = GetComponentInChildren<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {

    }

    public virtual void ReturnDafaultSpeed()
    {
        anim.speed = 1;
    }

    #region Velocity
    public virtual void setVelocity(float xVelocity, float yVelocity)
    {
        if (IsKnocked)
        { 
            return;
        }
        rb.velocity = new Vector2( xVelocity,  yVelocity);
        FlipController(xVelocity);
    }

    public virtual void ZeroVelocity()
    {
        if (IsKnocked)
        {
            return;
        }

        rb.velocity = new Vector2(0, 0);
    }

    #endregion

    #region Flip
    public virtual void FlipController(float x )
    {
        
        if (x > 0 && !facingRight)
        {
            Flip();
        }

        if (x < 0 & facingRight)
        {
            Flip();
        }
    }

    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlipped != null)
        { 
            onFlipped();
        }
    }

    public virtual void SetupDefaultFacingDir(int _direction)
    { 
        facingDir  = _direction;

        if (facingDir == -1)
        {
            facingRight = false;
        }



    }
    #endregion

    

    public virtual  bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);  //当在whatIsGround图层检测到物体时（即地面），IsGroundDetected的返回值为true,否则为false

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);//wallCheckDistance * facingDir 是为了在角色调转方向时也能对应检测墙壁
    //// Physics2D.Raycast 方法
    //// Physics2D.Raycast 是 Unity 的 2D 物理引擎中的一个方法，用于发射一条射线并检测它是否与任何碰撞体（collider）相交。这个方法非常常用来检测物体是否在某个范围内，或者是否有障碍物阻挡。
    //// public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
    ////    参数
    //                                //    origin：射线的起点坐标（Vector2 类型）。
    //                                //    direction：射线的方向（Vector2 类型）。
    //                                //    distance（可选）：射线的最大检测距离（默认为 Mathf.Infinity，即无限远）。
    //                                //    layerMask（可选）：层掩码，用于指定哪些层的物体会与射线相交（默认为所有层）。

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }

    public void DamageImpact()
    {
        //fx.StartCoroutine("FlashFx"); //移动至CharacterStats 的TakeDamage
        StartCoroutine("HitKnockBack");
        Debug.Log(gameObject.name + " was damaged");

    }

    public virtual void SetUpKnockDir(Transform _damagedireaction)
    {
        if (_damagedireaction.position.x > transform.position.x) //当攻击来自敌人右边时，要将敌人往左退
        {
            KnockBackDir = -1;
        }
        else if (_damagedireaction.position.x < transform.position.x) //当攻击来自敌人左边时，要将敌人往右退
        {
            KnockBackDir = 1;
        }
    }

    public void SetUpKnockBackPower(Vector2 _knockPower)
    { 
        KnockBackPower = _knockPower;
    }
    public virtual IEnumerator HitKnockBack()
    { 
        IsKnocked = true;

        rb.velocity = new Vector2(KnockBackPower.x * KnockBackDir, KnockBackPower.y);

        yield return new WaitForSeconds(KnockBcakDuration);

        IsKnocked = false;
        SetUpZeroKnockPower();
    }


    protected virtual void SetUpZeroKnockPower()
    { 
        
    }



    public virtual void Die()
    { 
        
    }
}
