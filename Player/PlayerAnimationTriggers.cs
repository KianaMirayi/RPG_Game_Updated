using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    public Player player => GetComponentInParent<Player>();

    public PlayerPrimaryAttackState primaryAttack;

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        //角色自身发出的音效可以在动画触发器中做
        

        //AudioManager.instance.PlaySfx(2,null); 

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                    //hit.GetComponent<Enemy>().DamageImpact();
                    //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.Damage.GetValue());
                }



                Inventory.Instance.GetTypeOfEquipment(EquipmentType.Weapon)?.Effect(_target.transform);
            }
        }
    }

    public void ThrowSword()
    {
        SkillManager.SkillInstance.Sword.CreatSword();
    }
    
}
