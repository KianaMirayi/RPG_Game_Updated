using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private string StatName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI StatValueText;
    [SerializeField] private TextMeshProUGUI StatNameText;


    public void OnValidate()
    {
        gameObject.name = "Stat - " + StatName;


        if (StatNameText != null)
        { 
            StatNameText.text = StatName;

        }



    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatValueUI();
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


    }
}
