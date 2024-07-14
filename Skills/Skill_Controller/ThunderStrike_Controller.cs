using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThunderStrike_Controller : MonoBehaviour
{
    [SerializeField] private CharacterStats TargetStats;
    [SerializeField] private float speed;
    private int Damage;

    private Animator anim;

    private bool Triggered = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();    
    }

    private void Update()
    {
        if (!TargetStats)
        {
            return;
        }

        if (Triggered)
        { 
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, TargetStats.transform.position, speed * Time.deltaTime);
        transform.right = transform.position - TargetStats.transform.position;

        if (Vector2.Distance(transform.position, TargetStats.transform.position) < 0.1f)
        {
            anim.transform.localPosition = new Vector3(0, 0.3f);
            anim.transform.localRotation = Quaternion.identity;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);


            Invoke("DamageThenSelfDestory", 0.2f);
            Triggered = true;
            anim.SetTrigger("Hit");
        }
    }

    public void SetUpThunder(int _damage, CharacterStats _targetStats)
    { 
        Damage = _damage;
        TargetStats = _targetStats;
    }


    private void DamageThenSelfDestory()
    {
        TargetStats.ApplyShock(true);
        TargetStats.TakeDamage(Damage);
        Destroy(gameObject,0.4f);        
    }
}
