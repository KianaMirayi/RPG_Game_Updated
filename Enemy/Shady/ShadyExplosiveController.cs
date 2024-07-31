using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadyExplosiveController : MonoBehaviour
{
    private Animator anim;
    private CharacterStats myStats;
    private float growSpeed = 15;
    private float MaxSize = 6;
    private float explosionRadius;

    private bool canGrow = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale,new Vector2(MaxSize, MaxSize), growSpeed * Time.deltaTime);
        }

        if (MaxSize - transform.localScale.x < 0.5f)
        { 
            canGrow = false;
            anim.SetTrigger("Exlpode");
        }
        
    }


    public void SetupExplosion(CharacterStats _stats, float _growSpeed,float _maxsize, float _radius)
    { 
        anim = GetComponent<Animator>();
        myStats = _stats;
        growSpeed = _growSpeed;
        MaxSize = _maxsize;
        explosionRadius = _radius;
    }

    public void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<CharacterStats>() != null)
            {
                hit.GetComponent<Entity>().SetUpKnockDir(transform);
                myStats.DoDamage(hit.GetComponent<CharacterStats>()); //DoDamageTo

                //hit.GetComponent<Enemy>().DamageImpact();由以下语句替代
                //player.stats.DoMagicDamage(hit.GetComponent<CharacterStats>());

                //ItemData_Equipment equipAmulet = Inventory.Instance.GetTypeOfEquipment(EquipmentType.Amulet);

                //if (equipAmulet != null)
                //{
                //    equipAmulet.Effect(hit.transform);
                //}

            }
        }

    }

    private void SelfDestory()
    {
        Destroy(gameObject);
    }

}
