using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackHole_Skill : Skill
{
    [SerializeField] public GameObject BlackHolePrefab;

    public BlackHole_Skill_Controller CurrentBlackHole;

    [SerializeField] private UI_SkillTreeSlot UnlockBlackHoleButton;
    public bool BlackHoleUnlocked;

    [Header("BlackHoleState info")]
    [SerializeField] public float MaxSize;
    [SerializeField] public float MinSize;
    [SerializeField] public float GrowSpeed;
    [SerializeField] public bool CanGrow;
    [SerializeField] public int CountOfAttacks;
    [SerializeField] public float CloneAttackCoolDown;
    [SerializeField] public float BlackHoleDuration;
    

    


    public override void Start()
    {
        base.Start();
        UnlockBlackHoleButton.GetComponent<Button>().onClick.AddListener(UnlockBlackHole);
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void CheckLoadedSkillUnlock() // 同步上局技能解锁情况
    {
        UnlockBlackHole();
    }

    public void UnlockBlackHole()
    {
        if (UnlockBlackHoleButton.unlocked)
        {
            BlackHoleUnlocked = true;
        }
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();


        GameObject newBlackHole = Instantiate(BlackHolePrefab, player.transform.position,Quaternion.identity);

        CurrentBlackHole = newBlackHole.GetComponent<BlackHole_Skill_Controller>();

        CurrentBlackHole.SetUpBlackHole(MaxSize, MinSize, GrowSpeed,CanGrow, CountOfAttacks,  CloneAttackCoolDown,BlackHoleDuration);

        //AudioManager.instance.PlaySfx(,player.transform);在这里放角色大招的音效
        
    }

    public bool BlackHoleSkillCompleted()
    {
        if (!CurrentBlackHole)
        { 
            return false;
        }

        if (CurrentBlackHole.PlayerCanExitState)
        { 
            CurrentBlackHole = null;
            return true;
        }

        return false;
    }

    public float GetBlackHoleRadius()
    {
        return MaxSize / 2;
    }
}
