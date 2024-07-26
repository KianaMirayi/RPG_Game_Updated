using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string id; // 为每一个检查点分配ID
    public bool activationStaus;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("生成检查点ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActiveCheckPoint();
            Debug.Log("检查点已激活");
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
