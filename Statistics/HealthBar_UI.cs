using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity Entity =>GetComponentInParent<Entity>();
    private RectTransform HealthBarTransform;
    private Slider slider;

    private CharacterStats CharacterStats =>GetComponentInParent<CharacterStats>();

    private void Start()
    {
        //Entity = GetComponentInParent<Entity>();
        HealthBarTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        //CharacterStats = GetComponentInParent<CharacterStats>();

        //Entity.onFlipped += FlipUI;
        //CharacterStats.onHpUpdate += UpdateHpUI;

        UpdateHpUI();
    }

    //private void Update()  使用Update太占用性能
    //{
    //    UpdateHpUI();
    //}

    

    private void UpdateHpUI()
    {
        slider.maxValue = CharacterStats.GetMaxHp(); 
        slider.value = CharacterStats.CurrentHp;
    }

   

    private void OnEnable()
    {
        Entity.onFlipped += FlipUI;
        CharacterStats.onHpUpdate += UpdateHpUI;
    }

    private void OnDisable()
    {
        if (Entity != null)
        {
            Entity.onFlipped -= FlipUI;
        }

        if (CharacterStats != null)
        {
            CharacterStats.onHpUpdate -= UpdateHpUI;
        }

        
        
    }
    private void FlipUI()
    {
        //Debug.Log("Entity is Flipped");
        HealthBarTransform.Rotate(0, 180, 0);

    }
}
