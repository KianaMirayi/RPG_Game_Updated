using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikeEffect_Controller : MonoBehaviour
{
    public PlayerStats playerStats;

    // Start is called before the first frame update
    public virtual void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        { 
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            playerStats.DoMagicDamage(enemyStats);
        }
    }


}
