using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string id; // Ϊÿһ���������ID
    public bool activationStaus;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("���ɼ���ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActiveCheckPoint();
            Debug.Log("�����Ѽ���");
            Debug.Log(id + activationStaus);
        }
    }
    public void ActiveCheckPoint()
    {
        if (activationStaus == false)
        {
            AudioManager.instance.PlaySfx(5, transform);
        }

        
        activationStaus = true;
        anim.SetBool("Active", true);
    }
}
