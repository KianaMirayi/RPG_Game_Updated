using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Crystal_Skill : Skill
{
    [SerializeField] public GameObject CrystalPrefab;

    public GameObject CurrentCrystal;

    [SerializeField]public float CrystalDuration;

    [Header("Crystal Simple")]
    [SerializeField] public UI_SkillTreeSlot unlockCrystalButton;
    public bool crystalUnlocked;


    [Header("Explosive Crystal")]
    [SerializeField] public bool CanExplode;
    [SerializeField] public UI_SkillTreeSlot unlockExplosiveCrystalButton;



    [Header("Moving Crystal")]
    [SerializeField] private bool CanMoveToEnemy;
    [SerializeField] private float MoveSpeed;
    [SerializeField] public UI_SkillTreeSlot unlockMovingCrystalButton;


    [Header("Multi Crystal")]
    [SerializeField] public bool CanUseMulti;
    [SerializeField] public int AmountOfCrystal;
    [SerializeField] public float MultiCrystalCoolDown;
    [SerializeField] public float UseTimeWindow;
    [SerializeField] public List<GameObject> CrystalLeft = new List<GameObject>();
    [SerializeField] public UI_SkillTreeSlot unlockMultiCrystalButton;


    [Header("Crystal Mirage")]
    [SerializeField] public UI_SkillTreeSlot unlockCloneInsteadCrystalButton;
    [SerializeField] private bool CloneInsteadCrystal;


    public override void Start()
    {
        base.Start();
        unlockCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        unlockExplosiveCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockExplosiveCrystal);
        unlockCloneInsteadCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCloneInsteadCrystal);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        unlockMultiCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMultiCrystal);

    }

    protected override void CheckLoadedSkillUnlock()
    {
        UnlockCrystal();
        UnlockExplosiveCrystal();
        UnlockCloneInsteadCrystal();
        UnlockMovingCrystal();
        UnlockMultiCrystal();
    }


    #region unlock skills
    public void UnlockCrystal()
    {
        if (unlockCrystalButton.unlocked)
        {
            crystalUnlocked = true;
        }
    }

    public void UnlockExplosiveCrystal()
    {
        if (unlockExplosiveCrystalButton.unlocked)
        {
            CanExplode = true;
        }
    }

    public void UnlockCloneInsteadCrystal()
    {
        if (unlockCloneInsteadCrystalButton.unlocked)
        { 
            CloneInsteadCrystal = true;
        }
    }


    public void UnlockMovingCrystal()
    {
        if (unlockMovingCrystalButton.unlocked)
        {
            CanMoveToEnemy = true;
        }
    }


    public void UnlockMultiCrystal()
    {
        if (unlockMultiCrystalButton.unlocked)
        {
            CanUseMulti = true;
        }
    }
    #endregion







    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
        { 
            return;
        }
        


        if (CurrentCrystal == null )
        {
            CreatCrystal();
        }
        else
        { 
            if (CanMoveToEnemy == true)
            {
                return;
            }

            Vector2 playerposition = player.transform.position;

            player.transform.position = CurrentCrystal.transform.position;

            CurrentCrystal.transform.position = playerposition;


            if (CloneInsteadCrystal)
            {
                SkillManager.SkillInstance.Clone.CreateClone(CurrentCrystal.transform, Vector3.zero);
                Destroy(CurrentCrystal);
            }
            else
            { 
                CurrentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
                
            }


        }


    }

    public void CreatCrystal()
    {
        CurrentCrystal = Instantiate(CrystalPrefab, player.transform.position, Quaternion.identity);

        Crystal_Skill_Controller crystal_Skill_Controller = CurrentCrystal.GetComponent<Crystal_Skill_Controller>(); 

        crystal_Skill_Controller.SetUpCrystal(CrystalDuration, CanExplode, CanMoveToEnemy, MoveSpeed, FindClosetEnemy(CurrentCrystal.transform),player);

        crystal_Skill_Controller.ChooseRandomEnemy();
    }

    public void CurrentCrystalChooseRandomTarget()
    { 
        CurrentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();
    }

    private void ReFillCrystal()
    {
        int amountToAdd = AmountOfCrystal - CrystalLeft.Count;
        for (int i = 0; i < amountToAdd; i++)
        {
            CrystalLeft.Add(CrystalPrefab);
        }
    }

    private bool CanUseMultiCrystal()
    {
        if (CanUseMulti)
        {
            if (CrystalLeft.Count > 0)
            {
                if (CrystalLeft.Count == AmountOfCrystal)
                {
                    Invoke("ReSetSkill", UseTimeWindow);
                }

                CoolDown = 0;

                GameObject crystalToSpawn = CrystalLeft[CrystalLeft.Count - 1];  //选择列表中最后一个元素

                GameObject newCrystal = Instantiate(crystalToSpawn,player.transform.position,Quaternion.identity);  //在玩家位置生成一个水晶，预制体使用列表中最后一个元素

                CrystalLeft.Remove(crystalToSpawn);  //将最后一个元素移除出列表

                newCrystal.GetComponent<Crystal_Skill_Controller>().SetUpCrystal
                    (CrystalDuration, CanExplode, CanMoveToEnemy, MoveSpeed, FindClosetEnemy(newCrystal.transform), player);

                if (CrystalLeft.Count <= 0)
                { 
                    CoolDown = MultiCrystalCoolDown;
                    ReFillCrystal();
                }

                return true;

            }
            
        }
        return false;
    }

    public void ReSetSkill()
    {
        if (CoolDownTimer > 0)
        { 
            return;
        }

        CoolDownTimer = MultiCrystalCoolDown;
        ReFillCrystal();
    }


}
