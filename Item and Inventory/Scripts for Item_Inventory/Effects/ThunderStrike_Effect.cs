using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Item Effect/Thunder Strike")]
public class ThunderStrike_Effect : ItemEffect
{
    [SerializeField] private GameObject ThunderStrikePrefab;

    public override void ExcuteEffect(Transform _enemyPosition)
    {
        GameObject newThunderStrike = Instantiate(ThunderStrikePrefab, _enemyPosition.position,Quaternion.identity);

        Destroy(newThunderStrike, 0.5f);
    }
}
