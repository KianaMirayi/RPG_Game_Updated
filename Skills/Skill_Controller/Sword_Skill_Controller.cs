using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    public Animator SwordAnim;

    public Rigidbody2D SwordRb;

    public CircleCollider2D SwordCd;

    public Player player;


    private bool CanRotate = true;

    public bool IsReturning;

    public float ReturnSpeed = 12;



    [Header("Bounce info")]
    private bool IsBouncing = false;  //剑在检测到多个敌人时在敌人中进行往返攻击
    private int AmountOfBounce;  //往返攻击次数
    private List<Transform> EnemyTarget;  //创建列表以存储检测到的敌人
    private int TargetIndex;  //列表索引
    [SerializeField] public float BounceSpeed;  //剑在敌人中往返速度


    [Header("Pirece info")]
    [SerializeField] public int AmountOfPirece;

    [Header("Spin info")]
    public float MaxTravelDistance;
    public float SpinDuration;
    public float SpinTimer;
    public bool isSpin;
    public bool isStopped;
    public float HitTimer;
    public float HitFrequency;







    private void Awake()
    {
        SwordAnim = GetComponentInChildren<Animator>();
        SwordRb = GetComponent<Rigidbody2D>();
        SwordCd = GetComponent<CircleCollider2D>();
    }

    public void SetUpSword(Vector2 dir, float gravityScale, Player _player)  //传入相关参数：剑的方向向量，重力以及使用主体
    {
        player = _player;
        SwordRb.velocity = dir;
        SwordRb.gravityScale = gravityScale;

        SwordAnim.SetBool("Rotation", true); //触发函数时播放剑的旋转动画

        
        Invoke("DestorySword", 4);  //若不回收剑，则剑在四秒后销毁

        

    }

    public void SetUpBounce(bool _isBoucing, int _amountOfBounce)
    { 
        IsBouncing = _isBoucing;
        AmountOfBounce = _amountOfBounce;

        EnemyTarget = new List<Transform>();
        
    }

    public void SetUpPirece(int amountOfPirece)
    { 
        AmountOfPirece = amountOfPirece;
    }

    public void SetUpSpin(bool _isSpin, float _maxTravelDistance, float _SpinDuration, float _HitFrequency)
    {
        isSpin = _isSpin;
        MaxTravelDistance = _maxTravelDistance;
        SpinDuration = _SpinDuration;
        HitFrequency = _HitFrequency;

    }

    public void ReturnSword()
    {
        //SwordRb.isKinematic = false;
        SwordRb.constraints = RigidbodyConstraints2D.FreezeAll; // 回收剑时冻结xyz属性
        transform.parent = null;
        IsReturning = true;
        SwordAnim.SetBool("Rotation", true);

    }

    private void Update()
    {
        if (CanRotate)
        {
            transform.right = SwordRb.velocity; //使对象的右侧方向对齐到当前速度方向。这通常用于确保对象朝向它的运动方向，例如使箭头、子弹或飞剑在飞行时始终朝向它们的前进方向。
        }

        if (IsReturning)  //回收剑
        {
            ReturnTheSwordToPlayer();
        }

        BounceLogic();

        SpinLogic();

        

    }

    private void ReturnTheSwordToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, ReturnSpeed * Time.deltaTime); //使剑从当前位置移动至玩家位置

        if (Vector2.Distance(transform.position, player.transform.position) < 1)  // 当剑于角色的距离小于1时，触发CatchTheSword函数
        {
            player.CatchTheSword();  // 在player脚本中声明TIme
        }
    }

    private void SpinLogic()
    {
        if (isSpin)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > MaxTravelDistance && !isStopped)
            {
                StopWhenSpin();
            }
        }

        if (isStopped)
        {
            SpinTimer -= Time.deltaTime;

            if (SpinTimer < 0)
            {
                IsReturning = true;
                isSpin = false;
            }
            HitTimer -= Time.deltaTime;
        }


        if (HitTimer < 0)
        {
            HitTimer = HitFrequency;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    //hit.GetComponent<Enemy>().DamageImpact();
                    player.stats.DoDamage(hit.GetComponent<CharacterStats>());

                    ItemData_Equipment equipAmulet = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Amulet);  //当装备上帝护身符时

                    if (equipAmulet != null)
                    {
                        equipAmulet.Effect(hit.transform);
                    }
                }
            }
        }
    }

    private void StopWhenSpin()
    {
        isStopped = true;
        SwordRb.constraints = RigidbodyConstraints2D.FreezePosition;
        SpinTimer = SpinDuration;
    }

    private void BounceLogic()
    {
        if (IsBouncing && EnemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, EnemyTarget[TargetIndex].position, BounceSpeed * Time.deltaTime);  //使剑在敌人中间往返，从当前位置移动至敌人列表索引位置

            if (Vector2.Distance(transform.position, EnemyTarget[TargetIndex].position) < 0.1f)  //当剑当前位置与敌人列表索引指向的位置之间的距离小于0.1时
            {
                //EnemyTarget[TargetIndex].GetComponent<Enemy>().DamageImpact();

                EnemyTarget[TargetIndex].GetComponent<Enemy>().stats.DoDamage(EnemyTarget[TargetIndex].GetComponent<CharacterStats>());//这么写对不对啊


                ItemData_Equipment equipAmulet = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Amulet);  //当装备上帝护身符时

                if (equipAmulet != null)
                {
                    equipAmulet.Effect(EnemyTarget[TargetIndex].transform);
                }

                TargetIndex++;  //敌人列表的索引值自增
                AmountOfBounce--;  //弹跳次数自减

                if (AmountOfBounce <= 0)
                {
                    IsBouncing = false;
                    IsReturning = true;
                }

                if (TargetIndex >= EnemyTarget.Count)  //当索引值大于等于敌人数量时，使索引值归零，即让剑回到检测到的第一个敌人的位置
                {
                    TargetIndex = 0;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) // 当另一个碰撞器2D进入了触发器，则调用OnTriggerEnter2D
    {
        if (IsReturning)
        {
            return;  //当触发该函数时，下面的语句全部不执行
        }

        //collision.GetComponent<Enemy>()?.DamageImpact();
        

        if (collision.GetComponent<Enemy>() != null)  //当检测到敌人碰撞器
        {
            if (IsBouncing && EnemyTarget.Count <= 0)  //当允许剑弹跳并且敌人列表尚未存储敌人信息
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 10);  //检测圈内所有碰撞体信息并将信息存储进数组中

                foreach (var hit in collider2Ds)  //遍历数组内容
                {
                    if (hit.GetComponent<Enemy>() != null)  //检测到敌人碰撞器
                    {
                        EnemyTarget.Add(hit.transform);  //将敌人的transform属性作为参数传入EnemyTarget列表中
                    }
                }
            }

            collision.GetComponent<Enemy>().stats.DoDamage(collision.GetComponent<CharacterStats>());
            ItemData_Equipment equipAmulet = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Amulet);

            if (equipAmulet != null)
            {
                equipAmulet.Effect(collision.transform);
            }
        }

        StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        if (AmountOfPirece > 0 && collision.GetComponent<Enemy>() != null)
        { 
            AmountOfPirece --;
            return;  // 该语句可以无视经过的敌人并最后在穿透次数为0时停止
        }

        if (isSpin)
        {
            StopWhenSpin();
            return;
        }

        CanRotate = false;

        SwordCd.enabled = false; // 冻结剑碰撞器

        SwordRb.isKinematic = true;  //将剑的Body Type变更为Kinematic

        //如果该属性设置为 true，则刚体将停止对碰撞和施加的力作出反应。对于通常情况下应该以“运动学”（即非物理）方式控制，但有时需要具有物理特性以增强真实感的对象，该属性很有用。例如，人类角色通常不使用物理实现，但有时可能会因冲击或爆炸而被抛到空中或与其他对象碰撞。

        SwordRb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (IsBouncing && EnemyTarget.Count > 0)
        {
            return ;
        }

        SwordAnim.SetBool("Rotation", false);
        transform.parent = collision.transform; // 剑插在敌人身上后将随敌人运动

        if (collision.GetComponent<Player>())
        {
            player.cd.enabled = true;

        }

        
        
    }

    public void DestorySword()
    {
        Destroy(gameObject);
    }
}
