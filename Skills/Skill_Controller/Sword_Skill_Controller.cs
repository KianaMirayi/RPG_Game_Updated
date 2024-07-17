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
    private bool IsBouncing = false;  //���ڼ�⵽�������ʱ�ڵ����н�����������
    private int AmountOfBounce;  //������������
    private List<Transform> EnemyTarget;  //�����б��Դ洢��⵽�ĵ���
    private int TargetIndex;  //�б�����
    [SerializeField] public float BounceSpeed;  //���ڵ����������ٶ�


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

    public void SetUpSword(Vector2 dir, float gravityScale, Player _player)  //������ز��������ķ��������������Լ�ʹ������
    {
        player = _player;
        SwordRb.velocity = dir;
        SwordRb.gravityScale = gravityScale;

        SwordAnim.SetBool("Rotation", true); //��������ʱ���Ž�����ת����

        
        Invoke("DestorySword", 4);  //�������ս����������������

        

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
        SwordRb.constraints = RigidbodyConstraints2D.FreezeAll; // ���ս�ʱ����xyz����
        transform.parent = null;
        IsReturning = true;
        SwordAnim.SetBool("Rotation", true);

    }

    private void Update()
    {
        if (CanRotate)
        {
            transform.right = SwordRb.velocity; //ʹ������Ҳ෽����뵽��ǰ�ٶȷ�����ͨ������ȷ�������������˶���������ʹ��ͷ���ӵ���ɽ��ڷ���ʱʼ�ճ������ǵ�ǰ������
        }

        if (IsReturning)  //���ս�
        {
            ReturnTheSwordToPlayer();
        }

        BounceLogic();

        SpinLogic();

        

    }

    private void ReturnTheSwordToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, ReturnSpeed * Time.deltaTime); //ʹ���ӵ�ǰλ���ƶ������λ��

        if (Vector2.Distance(transform.position, player.transform.position) < 1)  // �����ڽ�ɫ�ľ���С��1ʱ������CatchTheSword����
        {
            player.CatchTheSword();  // ��player�ű�������TIme
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

                    ItemData_Equipment equipAmulet = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Amulet);  //��װ���ϵۻ����ʱ

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
            transform.position = Vector2.MoveTowards(transform.position, EnemyTarget[TargetIndex].position, BounceSpeed * Time.deltaTime);  //ʹ���ڵ����м��������ӵ�ǰλ���ƶ��������б�����λ��

            if (Vector2.Distance(transform.position, EnemyTarget[TargetIndex].position) < 0.1f)  //������ǰλ��������б�����ָ���λ��֮��ľ���С��0.1ʱ
            {
                //EnemyTarget[TargetIndex].GetComponent<Enemy>().DamageImpact();

                EnemyTarget[TargetIndex].GetComponent<Enemy>().stats.DoDamage(EnemyTarget[TargetIndex].GetComponent<CharacterStats>());//��ôд�Բ��԰�


                ItemData_Equipment equipAmulet = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Amulet);  //��װ���ϵۻ����ʱ

                if (equipAmulet != null)
                {
                    equipAmulet.Effect(EnemyTarget[TargetIndex].transform);
                }

                TargetIndex++;  //�����б������ֵ����
                AmountOfBounce--;  //���������Լ�

                if (AmountOfBounce <= 0)
                {
                    IsBouncing = false;
                    IsReturning = true;
                }

                if (TargetIndex >= EnemyTarget.Count)  //������ֵ���ڵ��ڵ�������ʱ��ʹ����ֵ���㣬���ý��ص���⵽�ĵ�һ�����˵�λ��
                {
                    TargetIndex = 0;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) // ����һ����ײ��2D�����˴������������OnTriggerEnter2D
    {
        if (IsReturning)
        {
            return;  //�������ú���ʱ����������ȫ����ִ��
        }

        //collision.GetComponent<Enemy>()?.DamageImpact();
        

        if (collision.GetComponent<Enemy>() != null)  //����⵽������ײ��
        {
            if (IsBouncing && EnemyTarget.Count <= 0)  //�������������ҵ����б���δ�洢������Ϣ
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 10);  //���Ȧ��������ײ����Ϣ������Ϣ�洢��������

                foreach (var hit in collider2Ds)  //������������
                {
                    if (hit.GetComponent<Enemy>() != null)  //��⵽������ײ��
                    {
                        EnemyTarget.Add(hit.transform);  //�����˵�transform������Ϊ��������EnemyTarget�б���
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
            return;  // �����������Ӿ����ĵ��˲�����ڴ�͸����Ϊ0ʱֹͣ
        }

        if (isSpin)
        {
            StopWhenSpin();
            return;
        }

        CanRotate = false;

        SwordCd.enabled = false; // ���ὣ��ײ��

        SwordRb.isKinematic = true;  //������Body Type���ΪKinematic

        //�������������Ϊ true������彫ֹͣ����ײ��ʩ�ӵ���������Ӧ������ͨ�������Ӧ���ԡ��˶�ѧ��������������ʽ���ƣ�����ʱ��Ҫ����������������ǿ��ʵ�еĶ��󣬸����Ժ����á����磬�����ɫͨ����ʹ������ʵ�֣�����ʱ���ܻ�������ը�����׵����л�������������ײ��

        SwordRb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (IsBouncing && EnemyTarget.Count > 0)
        {
            return ;
        }

        SwordAnim.SetBool("Rotation", false);
        transform.parent = collision.transform; // �����ڵ������Ϻ�������˶�

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
