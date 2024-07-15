using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clone_Skill_Controller : MonoBehaviour
{

    private Player player;
    private SpriteRenderer sr;

    private Animator animator;


    //[SerializeField] private float CloneDuration;
    [SerializeField] private float LosingSpeed;
    private float CloneTimer;
    [SerializeField] private Transform AttackCheck;
    [SerializeField] private float AttackCheckRadius = 0.8f;
    private Transform ClosetEnemy;
    private bool CanDuplicateClone;
    private float PossibilityToDuplicateClone;
    private int facingDir = 1;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CloneTimer -= Time.deltaTime;

        if (CloneTimer < 0)
        {

            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime) * LosingSpeed); //ͨ����ͼ��alphaͼ������ã�ʹ����ʱ��͸��

            if (sr.color.a < 0)
            { 
                Destroy(gameObject);  //��ͼ��͸��֮�����ٸ���Ϸ�������ռ���ڴ�
            }
        }
    }
    public void SetUpClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 _offSet,Transform _closetEnemy, bool _canDuplicateClone, float _possibilityOfDuplicateClone,Player _player)  //�ú�����Clone_Skill�б�������
    {
        if (canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1, 3)); //Random.Range����ʱ����ҿ�����
        }
        transform.position = newTransform.position + _offSet;
        CloneTimer = cloneDuration;
        ClosetEnemy = _closetEnemy;
        CanDuplicateClone = _canDuplicateClone;
        PossibilityToDuplicateClone = _possibilityOfDuplicateClone;
        player = _player;

        FaceClosetTarget();  //���ڿ�¡���Ӱ��������ʱ�ĳ���
        AttackTrigger();
    }



    private void AnimationTrigger()
    {
        CloneTimer = -0.1f;  //����ΪС��0�󣬵������¼�����������ֱ��ʹ����͸������
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackCheck.position, AttackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                //hit.GetComponent<Enemy>().DamageImpact();  //����任Ϊ��һ��
                player.stats.DoDamage(hit.GetComponent<CharacterStats>());

                if (CanDuplicateClone)
                {
                    if (Random.Range(0, 100) < PossibilityToDuplicateClone)
                    {
                        SkillManager.SkillInstance.Clone.CreateClone(hit.transform, new Vector3(.5f * facingDir, 0));
                    }
                }
            }
        }
    }

    private void FaceClosetTarget()  //�ú�����SetUpClone�����е���
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        //float closetDistance = Mathf.Infinity;

        //foreach (var hit in colliders)
        //{
        //    if (hit.GetComponent<Enemy>() != null)
        //    {
        //        float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

        //        if (distanceToEnemy < closetDistance)
        //        {
        //            closetDistance = distanceToEnemy;
        //            ClosetEnemy = hit.transform;
        //        }
        //    }
        //}


        if (ClosetEnemy != null)
        {
            if (transform.position.x > ClosetEnemy.position.x)  //����¡���Ӱ��λ���ڵ��˵��ұ�ʱ���л���������Ե���
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }


    }

}
