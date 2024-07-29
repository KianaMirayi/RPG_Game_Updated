using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private float colorLosingRate;

    public void SetupAfterImage(float _losingRate, Sprite _spriteImage)
    { 
        sr = GetComponent<SpriteRenderer>();
        colorLosingRate = _losingRate;
    }

    private void Update()
    {
        float alpha = sr.color.a - colorLosingRate * Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);


        if (sr.color.a <= 0)
        { 
            Destroy(gameObject);
        }

    }
}
