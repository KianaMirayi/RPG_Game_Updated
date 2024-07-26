using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SwordType  // Ϊ������������һ��ö��
 { 
        Regular,
        Bounce,
        Pirece,
        Spin
 }

public class Sword_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] public UI_SkillTreeSlot UnlockRegularSwordButton;
    public bool RegularSwordUnlocked;
    [SerializeField] public GameObject SwordPrefab;
    [SerializeField] public Vector2 LaunchForce;
    [SerializeField] public float SwordGravity;

    public Vector2 FinalDir;

    [Header("Aim Dots")]
    [SerializeField] public int NumberOfDots;
    [SerializeField] public float SpaceBetweenDots;
    [SerializeField] public GameObject DotPrefab;
    [SerializeField] public Transform dotParent;

    public GameObject[] dots;  //����һ������洢��׼��

    [Header("Bounce info")]
    [SerializeField] public UI_SkillTreeSlot UnlockBounceSwordButton;
    //public bool BounceSwordUnlocked;
    [SerializeField] public int AmountOfBounce;
    [SerializeField] public float BounceGravity;

    [Header("Pierce info")]
    [SerializeField] public UI_SkillTreeSlot UnlockPireceSwordButton;
    //public bool PireceSwordUnlocked;
    [SerializeField] public int AmountOfPierce;
    [SerializeField] public float PierceGravity;

    [Header("Spin info")]
    [SerializeField] public UI_SkillTreeSlot UnlockSpinSwordButton;
    //public bool SpinSwordUnlocked;
    [SerializeField] public bool IsSpin;
    [SerializeField] public float MaxTravelDistance;
    [SerializeField] public float SpinDuration;
    [SerializeField] public float SpinGravity;
    [SerializeField] public float HitFrequency;


    [Header("����")]
    [SerializeField] public UI_SkillTreeSlot UnlockSlowButton;
    public bool SlowUnlocked;
    [SerializeField] public UI_SkillTreeSlot UnlockVulnerableButton;
    public bool VulnerableUnlocked;





    public SwordType SwordType = SwordType.Regular;  //Ĭ�Ͻ�������ΪRegular


    public override void Start()
    {

        base.Start();

        UnlockRegularSwordButton.GetComponent<Button>().onClick.AddListener(UnlockRegularSword);
        UnlockBounceSwordButton.GetComponent<Button>().onClick.AddListener(UnlockBounceSowrd);
        UnlockPireceSwordButton.GetComponent<Button>().onClick.AddListener(UnlockPireceSword);
        UnlockSpinSwordButton.GetComponent<Button>().onClick.AddListener(UnlockSpinSword);
        UnlockSlowButton.GetComponent<Button>().onClick.AddListener(UnlockSlow);
        UnlockVulnerableButton.GetComponent<Button>().onClick.AddListener(UnlockVulnerable);


        GenerateDots();
        SetUpGravity();
    }
    public override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            FinalDir = new Vector2(AimDirection().normalized.x * LaunchForce.x, AimDirection().normalized.y * LaunchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))  //��Update�и������λ��ʵʱ��ʾ��׼���λ��
        {
            for (int i = 0; i < dots.Length; i++) 
            {
                dots[i].transform.position = DotsPosition(i * SpaceBetweenDots);
            }
        }
    }

   


    #region Skill Unlock

    public void UnlockRegularSword()
    {
        if (UnlockRegularSwordButton.unlocked)
        {
            SwordType = SwordType.Regular;
            RegularSwordUnlocked = true;
        }
                
    }


    public void UnlockBounceSowrd()
    {
        if (UnlockBounceSwordButton.unlocked)
        {
            SwordType = SwordType.Bounce;
        }

    }

    public void UnlockPireceSword()
    {
        if (UnlockPireceSwordButton.unlocked)
        {
            SwordType = SwordType.Pirece;
        }
    }

    public void UnlockSpinSword()
    {
        if (UnlockSpinSwordButton.unlocked)
        {
            SwordType = SwordType.Spin;
        }
    }

    public void UnlockSlow()
    {
        if (UnlockSlowButton.unlocked)
        { 
            SlowUnlocked = true;
        }
    }

    public void UnlockVulnerable()
    {
        if (UnlockVulnerableButton.unlocked)
        { 
            VulnerableUnlocked = true;
        }
    }


    #endregion

    protected override void CheckLoadedSkillUnlock()
    {
        UnlockRegularSword();
        UnlockBounceSowrd();
        UnlockPireceSword();
        UnlockSpinSword();
        UnlockSlow();
        UnlockVulnerable();
    }

    public void CreatSword()  //����ʵ�ֶ�Sword_Skill_Controller�ű��ķ���
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
        Vector2 position = (Vector2)player.transform.position/*��ʼλ��*/ + new Vector2(AimDirection().normalized.x * LaunchForce.x,AimDirection().normalized.y * LaunchForce.y)/*���ٶ�����*/ * t/*ʱ��*/ + 0.5f * (Physics2D.gravity * SwordGravity)/*�������ٶ�*/ * (t * t);
        // position = initial?position + initial?xVelocity * t + 1/2 * gravity * t * t
        //��ʵ�����Ƕ�ά�ռ������������˶���λ�ƹ�ʽ���������ʽ�ǣ� S = S0 + V0 * t + 1/2 a * (t * t)
        //S������λ�ã�S0�ǳ�ʼλ�ã�V0�ǳ��ٶȣ�t��ʱ�䣬 a�Ǽ��ٶȣ��������ٶȣ�
        return position;
    }
    #endregion
}
