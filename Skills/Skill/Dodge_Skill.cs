using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Dodge_Skill : Skill
{
    [Header("����")]
    [SerializeField] public UI_SkillTreeSlot unlockDodgeButton;
    [SerializeField] public int EvasionAmount;
    public bool DodgeUnlocked;


    [Header("���ܻ���")]
    [SerializeField] public UI_SkillTreeSlot unlockDodgeMirageButton;
    public bool DodgeMirageUnlocked;

    public override void UseSkill()
    {
        base.UseSkill();
    }
    public override void Start()
    {
        base.Start();

        unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        unlockDodgeMirageButton.GetComponent<Button>().onClick.AddListener(UnlockDodgeMirage);
    }

    public void UnlockDodge()
    { 
        if (unlockDodgeButton.unlocked && !DodgeUnlocked) //��������ʱ���ӽ�ɫ������ֵ
        {
            player.stats.Evasion.AddModifier(EvasionAmount);
            Inventory.Instance.UpDateStatsUI();//�����ӵ�����ֵ���������ݽ���
            DodgeUnlocked = true;
        }
    }


    public void UnlockDodgeMirage()
    {
        if (unlockDodgeMirageButton.unlocked)
        {
            DodgeMirageUnlocked = true;
        }
    }

    public void CreatMirageOnDodge() //���ܳɹ��ٻ�����
    {
        if (DodgeMirageUnlocked)
        {
            SkillManager.SkillInstance.Clone.CreateClone(player.transform, new Vector3(2 * player.facingDir,0)); // �ڵ�������ٻ�һ������
        }
    }

}

