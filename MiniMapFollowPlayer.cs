using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollowPlayer : MonoBehaviour
{
    public Transform player; // 玩家对象的 Transform
    public Camera miniMapCamera; // 小地图摄像机
    public Vector3 offset; // 偏移量

    private void LateUpdate()
    {
        if (player != null)
        {
            // 设置小地图摄像机的位置，保持与玩家的相对位置
            Vector3 newPosition = player.position + offset;
            newPosition.z = miniMapCamera.transform.position.z; // 确保小地图摄像机保持在其Z轴位置
            miniMapCamera.transform.position = newPosition;
        }
    }
}
