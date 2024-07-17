using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal_Effect", menuName = "Data/Heal Effect")]
public class HealEffect : ItemEffect
{
    
    [SerializeField] private float healPercentage;

    public override void ExcuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHp() * healPercentage);

        playerStats.IncreaseHpBy(healAmount);
    }
}
