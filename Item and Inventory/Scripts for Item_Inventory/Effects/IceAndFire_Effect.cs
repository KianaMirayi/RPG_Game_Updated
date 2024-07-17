using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IceAndFire Effect", menuName = "Data/Item Effect/Ice And Fire")]
public class IceAndFire_Effect : ItemEffect
{
    [SerializeField] private GameObject IceAndFirePrefab;
    [SerializeField] private float xVelocity;

    public override void ExcuteEffect(Transform _respawnPosition)
    {
        //Transform playerTransform = PlayerManager.instance.player.transform;
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.attackState.comboCounter == 2;

        if (thirdAttack)
        {
            GameObject newIceAndFire = Instantiate(IceAndFirePrefab, _respawnPosition.position,player.transform.rotation);  //冰与火的效果旋转随玩家的朝向

            newIceAndFire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDir, 0);

            Destroy(newIceAndFire, 2);
        }

        

    }
}
