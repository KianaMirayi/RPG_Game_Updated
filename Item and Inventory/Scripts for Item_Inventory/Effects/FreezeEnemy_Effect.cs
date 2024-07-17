using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEnemy_Effect", menuName = "Data/FreezeEnemy Effect")]
public class FreezeEnemy_Effect : ItemEffect
{
    [SerializeField] private float freezeDuration;

    public override void ExcuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats.CurrentHp < playerStats.MaxHp.GetValue() * 0.1f && Inventory.Instance.CanUseArmor())  //当角色血量低于最大生命值的10%时，触发冻结效果，否则不触发
        {
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_enemyPosition.position, 5);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    hit.GetComponent<Enemy>().StartCoroutine("FreezeEnemyWhenVitalHitFromEnemy", freezeDuration);

                }
            }

        }
       
    }
}
