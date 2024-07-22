using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header ("格挡")]
    [SerializeField] public UI_SkillTreeSlot ParryUnlockButton;
    public bool parryUnlocked;

    [Header("恢复")]
    [SerializeField] public UI_SkillTreeSlot RestoreUnlockedButton;
    public bool restoreUnlocked;
    [Range(0f, 1f)]
    [SerializeField] public float restoreHpPercentage;


    [Header("幻象进攻")]
    [SerializeField] public UI_SkillTreeSlot ParryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked;

    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHp() * restoreHpPercentage);  //若解锁弹反成功回复生命值，则生命值回复量取决于一定比例的最大生命值
            player.stats.IncreaseHpBy(restoreAmount);
        }

    }

    public override void Start()
    {
        base.Start();

        ParryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        RestoreUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockRestore);
        ParryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);

    }


    public void UnlockParry()
    { 
        if (ParryUnlockButton.unlocked)
        {
            parryUnlocked = true;
        }
    }

    public void UnlockRestore()
    {
        if (RestoreUnlockedButton.unlocked)
        { 
            restoreUnlocked = true;
        }
    }

    public void UnlockParryWithMirage()
    { 
        if (ParryWithMirageUnlockButton.unlocked)
        {
            parryWithMirageUnlocked = true;
        }
    }

    public void CreateMirageOnParry(Transform _respwanTransform)  //允许召唤幻象
    {
        if (parryWithMirageUnlocked)
        {
            SkillManager.SkillInstance.Clone.CreateCloneWithDelay(_respwanTransform);
        }
    }

}
