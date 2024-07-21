using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private string StatName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI StatValueText;
    [SerializeField] private TextMeshProUGUI StatNameText;


    [TextArea]
    private UI ui;
    [SerializeField] private string StatDescription;
    [SerializeField] private string StatStory;  //For Test


    public void OnValidate()
    {
        gameObject.name = "Stat - " + StatName;


        if (StatNameText != null)
        {
            if (statType == StatType.health)
            {
                StatName = "生命值";  //将界面语言换成中文的  
                StatNameText.text = StatName;
            }

            if (statType == StatType.damage)
            {
                StatName = "攻击力";
                StatNameText.text = StatName;
            }

            if (statType == StatType.evasion)
            {
                StatName = "闪避";
                StatNameText.text = StatName;
            }

            if (statType == StatType.armor)
            {
                StatName = "护甲";
                StatNameText.text = StatName;
            }

            if (statType == StatType.magicalResistance)
            {
                StatName = "魔法抗性";
                StatNameText.text = StatName;
            }

            if (statType == StatType.critChance)
            {
                StatName = "暴击率";
                StatNameText.text = StatName;
            }

            if (statType == StatType.critPower)
            {
                StatName = "暴击伤害";
                StatNameText.text = StatName;
            }

            if (statType == StatType.fireDamage)
            {
                StatName = "火元素伤害";
                StatNameText.text = StatName;
            }

            if (statType == StatType.iceDamage)
            {
                StatName = "冰元素伤害";
                StatNameText.text = StatName;
            }

            if(statType == StatType.lightingDamage)
            {
                StatName = "雷元素伤害";
                StatNameText.text = StatName;
            }

            if (statType == StatType.strength)
            {
                StatName = " 力量";
                StatNameText.text = StatName;
            }

            if (statType == StatType.agility)
            {
                StatName = "敏捷";
                StatNameText.text = StatName;
            }

            if (statType == StatType.intelligence)
            {
                StatName = "智力";
                StatNameText.text = StatName;
            }

            if (statType == StatType.vitality)
            {
                StatName = "体力";
                StatNameText.text = StatName;
            }

            //StatNameText.text = StatName;

        }



    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatValueUI();

        ui = GetComponentInParent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            StatValueText.text = playerStats.GetStat(statType).GetValue().ToString();

        }

        if (statType == StatType.health)
        { 
            StatValueText.text = playerStats.GetMaxHp().ToString();
        }

        if (statType == StatType.damage)
        { 
            StatValueText.text = (playerStats.Damage.GetValue() + playerStats.Strength.GetValue()).ToString();
        }


        if (statType == StatType.critPower)
        {
            StatValueText.text = (playerStats.CritPower.GetValue() + playerStats.Strength.GetValue()).ToString();
        }

        if (statType == StatType.critChance)
        {
            StatValueText.text = (playerStats.CritChance.GetValue() + playerStats.Agility.GetValue()).ToString();
        }

        if (statType == StatType.evasion)
        {
            StatValueText.text = (playerStats.Evasion.GetValue() + playerStats.Agility.GetValue()).ToString();
        }

        if (statType == StatType.magicalResistance)
        {
            StatValueText.text = (playerStats.MagicResistance.GetValue() + (playerStats.Intelligence.GetValue() * 3)).ToString();
        }


    }

    public void OnPointerEnter(PointerEventData eventData)//鼠标悬停时触发详细信息
    {
        //Vector2 mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);

        //float xOffset = 0;
        //float yOffset = 0;

        //if (mousePosition.x > 600)
        //{
        //    xOffset = -150;
        //}
        //else
        //{
        //    xOffset = 350;
        //}

        //if (mousePosition.y > 650)
        //{
        //    yOffset = -80;
        //}
        //else
        //{
        //    yOffset = 80;
        //}



        ui.statDescription.ShowStatDescription(StatDescription);
        ui.statDescription.ShowStatStory(StatStory);//For Test
        //ui.statDescription.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);

    }

    public void OnPointerExit(PointerEventData eventData)//鼠标离开时隐藏详细信息
    {
        ui.statDescription.HideStatDescription();
        ui.statDescription.HideStatStory();//For Test
    }
}
