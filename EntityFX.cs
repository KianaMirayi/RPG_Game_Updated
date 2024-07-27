using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

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

    [Header("Ailment particles")]
    [SerializeField] private ParticleSystem igniteFX;
    [SerializeField] private ParticleSystem ChillFX;
    [SerializeField] private ParticleSystem ShockFX;

    [Header("Dust FX")]
    [SerializeField] private ParticleSystem dustFX;

    [Header("Hit FX")]
    [SerializeField] private GameObject HitFX1Prefab;
    [SerializeField] private GameObject CritHitFXPrefab;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMat = sr.material;
    }

    public void MakeTransparent(bool _transParent)
    {
        if (_transParent)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - Time.deltaTime * 1.5f);
            /*cd.enabled = false;*/  //������ײ��ʹ���˼�ⲻ��
        }
        else
        {
            sr.color = Color.white;
            //cd.enabled = true;
        }
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

        igniteFX.Stop();
        ChillFX.Stop();
        ShockFX.Stop();
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
        igniteFX.Play();

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
        ChillFX.Play();
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
        ShockFX.Play();
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    public void GenerateHitFX(Transform _targetTransform, bool _critical)
    {

        float zRotation = Random.Range(-90,90);
        float xPosition = Random.Range(-0.5f, 0.5f);
        float yPosition = Random.Range(-0.5f, 0.5f);

        Vector3 hitFxRotation = new Vector3(0, 0, zRotation);

        GameObject hitPrefab = HitFX1Prefab;

        if (_critical)
        {

            hitPrefab = CritHitFXPrefab;

            float yRatation = 0; //�����Ĺ�����Чβ��Ĭ�����ң����Ա���ʱ�������������峯���뱩����Чβ�������෴

            zRotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().facingDir == -1) //
            {
                yRatation = 180;
            }

            hitFxRotation = new Vector3(0, yRatation, zRotation);

        }


        GameObject newHitFX = Instantiate(hitPrefab, _targetTransform.position + new Vector3(xPosition,yPosition),Quaternion.identity);

        newHitFX.transform.Rotate(hitFxRotation);
        

        Destroy(newHitFX,1);
    }

    public void PlayDustFX()
    {
        if (dustFX != null)
        { 
            dustFX.Play();
        }
    }
    
}
