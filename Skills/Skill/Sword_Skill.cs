using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType  // 为剑的类型声明一个枚举
 { 
        Regular,
        Bounce,
        Pirece,
        Spin
 }

public class Sword_Skill : Skill
{
    [Header("Skill info")]

    [SerializeField] public GameObject SwordPrefab;
    [SerializeField] public Vector2 LaunchForce;
    [SerializeField] public float SwordGravity;

    public Vector2 FinalDir;

    [Header("Aim Dots")]
    [SerializeField] public int NumberOfDots;
    [SerializeField] public float SpaceBetweenDots;
    [SerializeField] public GameObject DotPrefab;
    [SerializeField] public Transform dotParent;

    public GameObject[] dots;  //声明一个数组存储瞄准点

    [Header("Bounce info")]
    [SerializeField] public int AmountOfBounce;
    [SerializeField] public float BounceGravity;

    [Header("Pierce info")]
    [SerializeField] public int AmountOfPierce;
    [SerializeField] public float PierceGravity;

    [Header("Spin info")]
    [SerializeField] public bool IsSpin;
    [SerializeField] public float MaxTravelDistance;
    [SerializeField] public float SpinDuration;
    [SerializeField] public float SpinGravity;
    [SerializeField] public float HitFrequency;






    public SwordType SwordType = SwordType.Regular;  //默认剑的类型为Regular


    public override void Start()
    {
        base.Start();
        GenerateDots();
        SetUpGravity();
    }
    public override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            FinalDir = new Vector2(AimDirection().normalized.x * LaunchForce.x, AimDirection().normalized.y * LaunchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))  //在Update中根据鼠标位置实时显示瞄准点的位置
        {
            for (int i = 0; i < dots.Length; i++) 
            {
                dots[i].transform.position = DotsPosition(i * SpaceBetweenDots);
            }
        }
    }


    public void CreatSword()  //这里实现对Sword_Skill_Controller脚本的访问
    {
        GameObject newSword = Instantiate(SwordPrefab, player.transform.position, transform.rotation);

        Sword_Skill_Controller sword_skill_controller = newSword.GetComponent<Sword_Skill_Controller>();


        if (SwordType == SwordType.Bounce)
        {
            //SwordGravity = BounceGravity;
            sword_skill_controller.SetUpBounce(true, AmountOfBounce);

        }
        else if (SwordType == SwordType.Pirece)
        {
            sword_skill_controller.SetUpPirece(AmountOfPierce);
        }
        else if (SwordType == SwordType.Spin)
        {
            sword_skill_controller.SetUpSpin(true, MaxTravelDistance, SpinDuration, HitFrequency);
        }


        sword_skill_controller.SetUpSword(FinalDir,SwordGravity, player);

        player.AssignNewSword(newSword);


        DotsActive(false);
    }

    public void SetUpGravity()
    {
        switch (SwordType)
        { 
            case SwordType.Bounce:
                SwordGravity = BounceGravity;
                break;
            case SwordType.Pirece:
                SwordGravity = PierceGravity;
                break;
            case SwordType.Spin:
                SwordGravity = SpinGravity;
                break;
        }
    }

    #region Aim
    public Vector2 AimDirection()
    { 
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }



    public void GenerateDots()
    {
        dots = new GameObject[NumberOfDots];
        for (int i = 0; i < NumberOfDots; i++)
        {
            dots[i] = Instantiate(DotPrefab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }

    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }

    public Vector2 DotsPosition(float t)
    { 
        Vector2 position = (Vector2)player.transform.position/*初始位置*/ + new Vector2(AimDirection().normalized.x * LaunchForce.x,AimDirection().normalized.y * LaunchForce.y)/*初速度向量*/ * t/*时间*/ + 0.5f * (Physics2D.gravity * SwordGravity)/*重力加速度*/ * (t * t);
        // position = initial?position + initial?velocity * t + 1/2 * gravity * t * t
        //这实际上是二维空间中物体抛射运动的位移公式，其基本形式是： S = S0 + V0 * t + 1/2 a * (t * t)
        //S是最终位置，S0是初始位置，V0是初速度，t是时间， a是加速度（重力加速度）
        return position;
    }
    #endregion
}
