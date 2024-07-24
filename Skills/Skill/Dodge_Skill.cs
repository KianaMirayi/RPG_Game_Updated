using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Dodge_Skill : Skill
{
    [Header("闪避")]
    [SerializeField] public UI_SkillTreeSlot unlockDodgeButton;
    [SerializeField] public int EvasionAmount;
    public bool DodgeUnlocked;


    [Header("闪避幻象")]
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
        if (unlockDodgeButton.unlocked && !DodgeUnlocked) //解锁闪避时增加角色的闪避值
        {
            player.stats.Evasion.AddModifier(EvasionAmount);
            Inventory.Instance.UpDateStatsUI();//将增加的闪避值更新至数据界面
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

    public void CreatMirageOnDodge() //闪避成功召唤幻象
    {
        if (DodgeMirageUnlocked)
        {
            SkillManager.SkillInstance.Clone.CreateClone(player.transform, new Vector3(2 * player.facingDir,0)); // 在敌人身后召唤一个幻象
        }
    }

}

