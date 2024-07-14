using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash Fx")]
    [SerializeField] private Material HitMat;
    [SerializeField] private float FlashDuration;
    private Material originMat;

    [Header("Ailment Color")]
    [SerializeField] private Color[] ChillColor;
    [SerializeField] private Color[] IgniteColor;
    [SerializeField] private Color[] ShockColor;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMat = sr.material;
    }

    private IEnumerator FlashFx()
    {
        sr.material = HitMat;
        Color currentcolor = sr.color;

        yield return new WaitForSeconds(FlashDuration);

        sr.color = currentcolor;
        sr.material = originMat;

        
    }

    public void RedBlink()
    {
        if (sr.color != Color.white) // 当颜色不是白色，即当前颜色是红色的时候=》变成白色
        {
            sr.color = Color.white;
        }
        else  //当颜色是白色的时候=》变成红色
        {
            sr.color = Color.red;
        }

    }

    public void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }


    public void IgniteColorFx()  //燃烧特效
    {
        if (sr.color != IgniteColor[0])
        {
            sr.color = IgniteColor[0];
        }
        else
        {
            sr.color = IgniteColor[1];
        }
    }

    public void IgniteFxFor(float _seconds)  //燃烧特效调用
    {
        InvokeRepeating("IgniteColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    public void ChillColorFx()  //冰冻特效
    {
        if (sr.color != ChillColor[0])
        {
            sr.color = ChillColor[0];
        }
        else
        {
            sr.color = ChillColor[1];
        }
    }

    public void ChillFxFor(float _seconds)  //冰冻特效调用
    {
        InvokeRepeating("ChillColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    public void ShockColorFx()  //雷电特效
    {
        if (sr.color != ShockColor[0])
        {
            sr.color = ShockColor[0];
        }
        else
        { 
            sr.color = ShockColor[1];
        }
    }

    public void ShockFxFor(float _seconds)  //雷电特效调用
    {
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }
}
