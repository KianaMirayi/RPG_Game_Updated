using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollowPlayer : MonoBehaviour
{
    public Transform player; // ��Ҷ���� Transform
    public Camera miniMapCamera; // С��ͼ�����
    public Vector3 offset; // ƫ����

    private void LateUpdate()
    {
        if (player != null)
        {
            // ����С��ͼ�������λ�ã���������ҵ����λ��
            Vector3 newPosition = player.position + offset;
            newPosition.z = miniMapCamera.transform.position.z; // ȷ��С��ͼ�������������Z��λ��
            miniMapCamera.transform.position = newPosition;
        }
    }
}
