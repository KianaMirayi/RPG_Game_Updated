using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{


    //[Header("Screen Shake")]
    //private CinemachineImpulseSource screenShake;
    //[SerializeField] public float shakeMultiplier;
    //public Vector3 swordShakeImpact;
    //public Vector3 highDamageShakeImpact;
    //public Vector3 critHitShakeImpact;
    //public Vector3 DangerShakeImpact; //攻击大于当前生命值的30%时

    //[Header("After Image FX")]  //
    //[SerializeField] private GameObject afterImagePrefab;
    //[SerializeField] private float colorLosingRate;
    //[SerializeField] private float afterImageCoolDown;
    //private float afterImageCoolDownTimer;


    //[Header("Dust FX")] // copy to playerfx
    //[SerializeField] private ParticleSystem dustFX;
    //[SerializeField] private ParticleSystem dashFX;


    //protected override void Start()
    //{
    //    base.Start();
    //    screenShake = GetComponent<CinemachineImpulseSource>();
    //}

    //private void Update()
    //{
    //    afterImageCoolDownTimer -= Time.deltaTime;
    //}



    //public void ScreenShake(Vector3 _shakePower) //只要想让屏幕抖动，就调用这个方法
    //{
    //    screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
    //    screenShake.GenerateImpulse();
    //}

    //public void CreateAfterImage() //copy to playerfx
    //{
    //    if (afterImageCoolDownTimer <= 0)
    //    {
    //        afterImageCoolDownTimer = afterImageCoolDown;

    //        GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
    //        newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLosingRate, sr.sprite);
    //    }

    //}


    //public void PlayDustFX()
    //{
    //    if (dustFX != null)
    //    {
    //        dustFX.Play();
    //    }
    //}

    //public void PlayDashFX()
    //{
    //    if (dashFX != null)
    //    {
    //        dashFX.Play();
    //    }
    //}

}
