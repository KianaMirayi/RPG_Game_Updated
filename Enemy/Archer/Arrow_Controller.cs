using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "Player"; //��ͼ��������ͬ
    [SerializeField] private int damage;

    [SerializeField] private float xVelocity;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private bool canMove = true;
    [SerializeField] private bool flipped;
    private int facingDir = 1;

    private CharacterStats myStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        }

        if (facingDir == 1 && rb.velocity.x < 0)
        {
            facingDir = -1;
            sr.flipX = true;
        }
    }

    public void SetupArrow(float _speed,CharacterStats _myStats)
    { 
        sr = GetComponent<SpriteRenderer>();
        xVelocity = _speed;
        
        myStats = _myStats;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            //collision.GetComponent<CharacterStats>()?.TakeDamage(damage);
            myStats.DoDamage(collision.GetComponent<CharacterStats>()); //DoDamageTo

            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            StuckInto(collision);
        }
    }

    private void StuckInto(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false; //��ֻ����һ���˺�
        canMove = false;
        rb.isKinematic = true; //�Ѽ��̶��ڵ�������
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;

        Debug.Log(transform.parent);

        Destroy(gameObject, Random.Range(2, 3));
    }

    public void FlipArrow()
    {
        if (flipped)
        {
            return;
        }

        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayerName = "Enemy";
    }
}
