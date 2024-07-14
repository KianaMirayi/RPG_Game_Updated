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
        if (sr.color != Color.white) // ����ɫ���ǰ�ɫ������ǰ��ɫ�Ǻ�ɫ��ʱ��=����ɰ�ɫ
        {
            sr.color = Color.white;
        }
        else  //����ɫ�ǰ�ɫ��ʱ��=����ɺ�ɫ
        {
            sr.color = Color.red;
        }

    }

    public void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }


    public void IgniteColorFx()  //ȼ����Ч
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

    public void IgniteFxFor(float _seconds)  //ȼ����Ч����
    {
        InvokeRepeating("IgniteColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    public void ChillColorFx()  //������Ч
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

    public void ChillFxFor(float _seconds)  //������Ч����
    {
        InvokeRepeating("ChillColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    public void ShockColorFx()  //�׵���Ч
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

    public void ShockFxFor(float _seconds)  //�׵���Ч����
    {
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }
}
