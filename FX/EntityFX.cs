using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using Cinemachine;
using UnityEngine.UIElements;
using TMPro;

public class EntityFX : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Player player;

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

    [Header("Dust FX")] // copy to playerfx
    [SerializeField] private ParticleSystem dustFX;
    [SerializeField] private ParticleSystem dashFX;

    [Header("Hit FX")]
    [SerializeField] private GameObject HitFX1Prefab;
    [SerializeField] private GameObject CritHitFXPrefab;

    [Header("After Image FX")]  //copy to player fx
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLosingRate;
    [SerializeField] private float afterImageCoolDown;
    private float afterImageCoolDownTimer;

    [Header("Screen Shake")]  //copy to playerfx
    private CinemachineImpulseSource screenShake;
    [SerializeField] public float shakeMultiplier;
    public Vector3 swordShakeImpact;
    public Vector3 highDamageShakeImpact;
    public Vector3 critHitShakeImpact;
    public Vector3 DangerShakeImpact; //攻击大于当前生命值的30%时

    [Header("Popup Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    private GameObject healthBar;
    




    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMat = sr.material;
        player = PlayerManager.instance.player;
        screenShake = GetComponent<CinemachineImpulseSource>(); //copy to playerfx
        healthBar = GetComponentInChildren<HealthBar_UI>().gameObject;


    }

    private void Update()
    {
        afterImageCoolDownTimer -= Time.deltaTime; //copy to playerfx
    }

    public void ScreenShake(Vector3 _shakePower) //只要想让屏幕抖动，就调用这个方法  //copy to playerfx
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }

    public void MakeTransparent(bool _transParent)
    {
        if (_transParent)
        {
            healthBar.SetActive(false);
            sr.color = new Color(1, 1, 1, sr.color.a - Time.deltaTime * 1.5f);
            /*cd.enabled = false;*/  //冻结碰撞器使敌人检测不到
        }
        else
        {
            healthBar.SetActive(true);
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

    public void GenerateHitFX(Transform _targetTransform, bool _critical) //攻击与暴击特效
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

    public void PlayDustFX() //copy to playerfx
    {
        if (dustFX != null)
        {
            dustFX.Play();
        }
    }

    public void PlayDashFX() //copy to playerfx
    {
        if (dashFX != null)
        {
            dashFX.Play();
        }
    }

    public void CreateAfterImage() //copy to playerfx
    {
        if (afterImageCoolDownTimer <= 0)
        {
            afterImageCoolDownTimer = afterImageCoolDown;

            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLosingRate, sr.sprite);
        }

    }

    public void CreatePopUpText(string _text) //需要文字漂浮时调用此方法
    {
        float xRandom = Random.Range(-1, 1);
        float yRandom = Random.Range(1, 3);

        Vector3 positionOffset = new Vector3(xRandom, yRandom, 0);

        GameObject newText = Instantiate(popUpTextPrefab,transform.position + positionOffset, Quaternion.identity);
        newText.GetComponent<TextMeshPro>().text = _text;

        
    }
    
}
