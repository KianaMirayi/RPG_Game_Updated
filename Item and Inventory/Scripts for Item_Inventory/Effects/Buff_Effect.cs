using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;




[CreateAssetMenu(fileName ="Buff_Effect", menuName = "Data/Buff Effect")]
public class Buff_Effect : ItemEffect
{
    private PlayerStats playerStats;
    [SerializeField] private StatType BuffType;
    [SerializeField] private int BuffAmount;
    [SerializeField] private float BuffDuration;

    public override void ExcuteEffect(Transform _enemyPosition)
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.IncreaseStatBy(BuffAmount, BuffDuration, playerStats.GetStat(BuffType));
    }

   

}
