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
            /*cd.enabled = false;*/  //冻结碰撞器使敌人检测不到
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

        igniteFX.Stop();
        ChillFX.Stop();
        ShockFX.Stop();
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
        igniteFX.Play();

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
        ChillFX.Play();
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

            float yRatation = 0; //暴击的攻击特效尾巴默认向右，所以暴击时做出暴击的物体朝向与暴击特效尾部朝向相反

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
