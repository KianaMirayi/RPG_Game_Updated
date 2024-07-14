using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    public Animator CrystalAnim;
    public CircleCollider2D CrystalCd;

    public float CrystalExitTimer;

    private bool CanExplode;
    private bool CanMoveToEnemy;
    private float MoveSpeed;

    public bool CanGrow;
    public float GrowSpeed;

    private Transform ClosetEnemy;
    [SerializeField] private LayerMask WhatIsEnemy;


    private void Awake()
    {
        CrystalAnim = GetComponent<Animator>();
        CrystalCd = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        CrystalExitTimer -= Time.deltaTime;

        if (CrystalExitTimer < 0)
        {
            FinishCrystal();

        }

        if (CanGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.2f, 1.2f), GrowSpeed * Time.deltaTime);
        }

        if (CanMoveToEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, ClosetEnemy.position, MoveSpeed * Time.deltaTime * 2);
            if (Vector2.Distance(transform.position,ClosetEnemy.position) < 1)
            {
                FinishCrystal();
                CanMoveToEnemy = false;
            }
        }
    }

    public void FinishCrystal()
    {
        if (CanExplode)
        {
            CanGrow = true;
            CrystalAnim.SetTrigger("Explode");
        }
        else
        {
            CrystalSelfDestory();
        }
    }

    public void SetUpCrystal(float _crystalDuration, bool _canExplode, bool _canMoveToEnemy, float _moveSpeed, Transform _closetEnemy)
    { 
        CrystalExitTimer = _crystalDuration;
        CanExplode = _canExplode;
        CanMoveToEnemy = _canMoveToEnemy;
        MoveSpeed = _crystalDuration;
        ClosetEnemy = _closetEnemy;
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager.SkillInstance.BlackHole.GetBlackHoleRadius();

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position,radius,WhatIsEnemy);

        if (collider.Length > 0)
        { 
            ClosetEnemy = collider[Random.Range(0,collider.Length)].transform;
            
        }
    }


    public void CrystalSelfDestory()
    {
        Destroy(gameObject);
    }

    public void CrystalAnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, CrystalCd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().DamageEffect();
            }
        }

    }


}
