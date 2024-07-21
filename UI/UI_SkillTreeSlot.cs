using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;
    private Image SkillImage;

    [SerializeField] private string skillName;

    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;


    public void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI _ " + skillName;
    }

    public void Start()
    {
        SkillImage = GetComponent<Image>();

        SkillImage.color = lockedSkillColor;

        GetComponent<Button>().onClick.AddListener(() => UnLockSkillSlot());

        ui = GetComponentInParent<UI>();
    }


    public void UnLockSkillSlot()
    {
        for (int i = 0; i < shouldBeUnlocked.Length; i++)  //应该被解锁的没有解锁
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Can't unlock Skill");
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; i++)  //应该锁定的被解锁了
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                Debug.Log("Cannot unlock SKill");
                return;
            }
        }

        unlocked = true;
        SkillImage.color = Color.white;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillDescription.ShowDescripton(skillDescription,skillName);

        Vector2 mousePosition = Input.mousePosition;
        Debug.Log(mousePosition);

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 500)
        {
            xOffset = -350;
        }
        else
        {
            xOffset = 350;
        }

        if (mousePosition.y > 500)
        {
            yOffset = -250;
        }
        else
        {
            yOffset = 250;
        }

        ui.skillDescription.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset); // 鼠标进入时界面适应鼠标位置

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillDescription.HideDescription();
    }
}
