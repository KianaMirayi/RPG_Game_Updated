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

            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime) * LosingSpeed); //通过对图像alpha图层的设置，使其随时间透明

            if (sr.color.a < 0)
            { 
                Destroy(gameObject);  //当图像透明之后，销毁该游戏对象避免占用内存
            }
        }
    }
    public void SetUpClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 _offSet,Transform _closetEnemy, bool _canDuplicateClone, float _possibilityOfDuplicateClone,Player _player)  //该函数在Clone_Skill中被调用了
    {
        if (canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1, 3)); //Random.Range方法时左闭右开区间
        }
        transform.position = newTransform.position + _offSet;
        CloneTimer = cloneDuration;
        ClosetEnemy = _closetEnemy;
        CanDuplicateClone = _canDuplicateClone;
        PossibilityToDuplicateClone = _possibilityOfDuplicateClone;
        player = _player;

        FaceClosetTarget();  //用于克隆体幻影攻击敌人时的朝向
        AttackTrigger();
    }



    private void AnimationTrigger()
    {
        CloneTimer = -0.1f;  //设置为小于0后，当动画事件被触发，将直接使进入透明代码
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackCheck.position, AttackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                //hit.GetComponent<Enemy>().DamageImpact();  //该语句换为下一句
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

    private void FaceClosetTarget()  //该函数在SetUpClone函数中调用
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
            if (transform.position.x > ClosetEnemy.position.x)  //当克隆体幻影的位置在敌人的右边时，切换朝向以面对敌人
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }


    }

}
