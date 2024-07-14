using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]public float CoolDown;  //针对不同技能实现设定的冷却时间
    public float CoolDownTimer;  //不同技能释放后随时间减少

    public Player player;

    public virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    public virtual void Update()
    {
        CoolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (CoolDownTimer < 0)  //当技能释放后
        {
            UseSkill();  // 调用技能
            CoolDownTimer = CoolDown;  //计时器重置
            return true;
        }
        Debug.Log("Skill is on CoolDown");
        return false;

    }

    public virtual void UseSkill()
    {
        Debug.Log("Do some specific skills");
    }

    public virtual Transform FindClosetEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        float closetDistance = Mathf.Infinity;

        Transform ClosetEnemy  = null;
        
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                if (distanceToEnemy < closetDistance)
                { 
                    closetDistance = distanceToEnemy;
                    ClosetEnemy = hit.transform;
                }
            }
        }

        return ClosetEnemy;
    }


}
