using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpTextFX : MonoBehaviour
{
    private TextMeshPro text;

    [SerializeField] private float speed;
    [SerializeField] private float disappearingSpeed;
    [SerializeField] private float colorDisapperingSpeed;

    [SerializeField] private float lifeTime;


    private float textTimer;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        textTimer = lifeTime;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        textTimer -= Time.deltaTime;

        if (textTimer < 0)
        { 
            float alpha = text.color.a - colorDisapperingSpeed * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b,alpha);

            EnemyStats enemyStats = FindAnyObjectByType<EnemyStats>();

            if (enemyStats.IsIgnited)
            {
                text.color = new Color(255, 128,0, alpha);
                text.fontSize = 6;
                Debug.Log("fffffffffffff");
            }
            if (enemyStats.IsShocked)
            {
                text.color = new Color(255, 255, 0, alpha);
                text.fontSize = 6;
                Debug.Log("ssssssssss");
            }
            if (enemyStats.IsChilled)
            {
                text.color = new Color(0,128, 255, alpha);
                text.fontSize = 6;
                Debug.Log("iiiiii");
            }

            if (PlayerManager.instance.player.GetComponent<CharacterStats>().CanCrit())
            {
                
                text.fontSize = 8;
                Debug.Log("ccccccc");
            }




            if (text.color.a < 50)
            {
                speed = disappearingSpeed;
            }

            if (text.color.a <= 0)
            { 
                Destroy(gameObject);
            }
            
        }
    }
}
