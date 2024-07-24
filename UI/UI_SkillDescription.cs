using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_SkillDescription : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI SkillText;
    [SerializeField] private TextMeshProUGUI SkillName;
    [SerializeField] private TextMeshProUGUI SkillCost;
    [SerializeField] private float defaultNameFontSize;
    [SerializeField] private float defaultTextFontSize;


    public void ShowDescripton(string _skillDescription, string _skillName, int _skillCost)
    { 
        SkillText.text = _skillDescription;
        SkillName.text = _skillName;
        SkillCost.text = "Cost: " + _skillCost;
        gameObject.SetActive(true);

        AdjustPositionForDescription();
        AdjustFontSize(SkillText);
        AdjustFontSize(SkillName);
    }

    public void HideDescription()
    {
        SkillName.fontSize = defaultNameFontSize;
        SkillText.fontSize = defaultTextFontSize;
        SkillText.text = "";
        gameObject.SetActive(false);
    }
}
