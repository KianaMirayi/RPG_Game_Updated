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
    [SerializeField] protected Vector2 KnockBackPower;  //���ڱ�ʾ������ʱ��λ������
    [SerializeField] protected float KnockBcakDuration;  //���ڱ�ʾ�ڱ�����״̬�µ�ʱ��
    public bool IsKnocked;  // �����ж��Ƿ񱻻���



    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int KnockBackDir { get; private set; }

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipped;  //ί��
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

    

    public virtual  bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);  //����whatIsGroundͼ���⵽����ʱ�������棩��IsGroundDetected�ķ���ֵΪtrue,����Ϊfalse

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);//wallCheckDistance * facingDir ��Ϊ���ڽ�ɫ��ת����ʱҲ�ܶ�Ӧ���ǽ��
    //// Physics2D.Raycast ����
    //// Physics2D.Raycast �� Unity �� 2D ���������е�һ�����������ڷ���һ�����߲�������Ƿ����κ���ײ�壨collider���ཻ����������ǳ���������������Ƿ���ĳ����Χ�ڣ������Ƿ����ϰ����赲��
    //// public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
    ////    ����
    //                                //    origin�����ߵ�������꣨Vector2 ���ͣ���
    //                                //    direction�����ߵķ���Vector2 ���ͣ���
    //                                //    distance����ѡ�������ߵ��������루Ĭ��Ϊ Mathf.Infinity��������Զ����
    //                                //    layerMask����ѡ���������룬����ָ����Щ���������������ཻ��Ĭ��Ϊ���в㣩��

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }

    public void DamageImpact()
    {
        //fx.StartCoroutine("FlashFx"); //�ƶ���CharacterStats ��TakeDamage
        StartCoroutine("HitKnockBack");
        Debug.Log(gameObject.name + " was damaged");

    }

    public virtual void SetUpKnockDir(Transform _damagedireaction)
    {
        if (_damagedireaction.position.x > transform.position.x) //���������Ե����ұ�ʱ��Ҫ������������
        {
            KnockBackDir = -1;
        }
        else if (_damagedireaction.position.x < transform.position.x) //���������Ե������ʱ��Ҫ������������
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
