using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SkillText;
    [SerializeField] private TextMeshProUGUI SkillName;


    public void ShowDescripton(string _skillDescription, string _skillName)
    { 
        SkillText.text = _skillDescription;
        SkillName.text = _skillName;
        gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        SkillText.text = "";
        gameObject.SetActive(false);
    }
}
