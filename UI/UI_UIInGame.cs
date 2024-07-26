using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_UIInGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image ParryImage;
    [SerializeField] private Image CrystalImage;
    [SerializeField] private Image SwordImage;
    [SerializeField] private Image BlackHoleImage;
    [SerializeField] private Image FlaskImage;

    [Header("Souls info")]
    [SerializeField] private TextMeshProUGUI CurrentCurrency;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 100;

    

    private SkillManager skills;

    // Start is called before the first frame update
    void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHpUpdate += UpdateHpUI;
        }

        

        skills = SkillManager.SkillInstance;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && skills.Dash.dashUnlocked)  //�����ȴͼ��
        {
            SetCoolDown(dashImage);
        }



        if (Input.GetKeyDown(KeyCode.Q) && skills.Parry.parryUnlocked) //����ȴͼ��
        {
            SetCoolDown(ParryImage);
        }



        if (Input.GetKeyDown(KeyCode.E) && skills.Crystal.crystalUnlocked) //ˮ����ȴͼ��
        {
            SetCoolDown(CrystalImage);
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) && skills.Sword.RegularSwordUnlocked) //����ȴͼ��
        {
            SetCoolDown(SwordImage);
        }


        if (Input.GetKeyDown(KeyCode.R) && skills.BlackHole.BlackHoleUnlocked)//������ȴͼ��
        {
            SetCoolDown(BlackHoleImage);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.Instance.GetTypeOfEquipment(EquipmentType.Flask) != null) //ҩˮ��ȴͼ��
        {
            SetCoolDown(FlaskImage);
        }


        CheckCoolDownOf(dashImage, skills.Dash.CoolDown);
        CheckCoolDownOf(ParryImage, skills.Parry.CoolDown);
        CheckCoolDownOf(CrystalImage, skills.Crystal.CoolDown);
        CheckCoolDownOf(SwordImage, skills.Sword.CoolDown);
        CheckCoolDownOf(BlackHoleImage, skills.BlackHole.CoolDown);
        CheckCoolDownOf(FlaskImage, Inventory.Instance.FlaskCoolDown);

        UpdateSoulsUI();

    }

    private void UpdateSoulsUI()
    {
        if (soulsAmount < PlayerManager.instance.GetCurrentCurrency())
        {
            soulsAmount += Time.deltaTime * increaseRate;
        }
        else
        {
            soulsAmount = PlayerManager.instance.GetCurrentCurrency();
        }


        CurrentCurrency.text = ((int)soulsAmount).ToString();
    }

    private void UpdateHpUI()
    {
        slider.maxValue = playerStats.GetMaxHp();
        slider.value = playerStats.CurrentHp;
    }

    private void SetCoolDown(Image _image)  //��ͼƬ��fillamount���в���
    {
        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }

       
    }

    private void CheckCoolDownOf(Image _image,float _coolDown)
    {
        if (_image.fillAmount > 0)
        { 
            _image.fillAmount -= 1/_coolDown * Time.deltaTime;
        }
    }


}
