using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Skill : Skill
{
    [SerializeField] public GameObject BlackHolePrefab;

    public BlackHole_Skill_Controller CurrentBlackHole;

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
    }

    public override void Update()
    {
        base.Update();
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        GameObject newBlackHole = Instantiate(BlackHolePrefab, player.transform.position,Quaternion.identity);

        CurrentBlackHole = newBlackHole.GetComponent<BlackHole_Skill_Controller>();

        CurrentBlackHole.SetUpBlackHole(MaxSize, MinSize, GrowSpeed,CanGrow, CountOfAttacks,  CloneAttackCoolDown,BlackHoleDuration);

        base.UseSkill();
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
