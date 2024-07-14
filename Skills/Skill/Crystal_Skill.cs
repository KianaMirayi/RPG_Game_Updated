using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Crystal_Skill : Skill
{
    [SerializeField] public GameObject CrystalPrefab;

    public GameObject CurrentCrystal;

    [SerializeField]public float CrystalDuration;


    [Header("Explosive Crystal")]
    [SerializeField] public bool CanExplode;


    [Header("Moving Crystal")]
    [SerializeField] private bool CanMoveToEnemy;
    [SerializeField] private float MoveSpeed;

    [Header("Multi Crystal")]
    [SerializeField] public bool CanUseMulti;
    [SerializeField] public int AmountOfCrystal;
    [SerializeField] public float MultiCrystalCoolDown;
    [SerializeField] public float UseTimeWindow;
    [SerializeField] public List<GameObject> CrystalLeft = new List<GameObject>();

    [Header("Crystal Mirage")]
    [SerializeField] private bool CloneInsteadCrystal;






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

        crystal_Skill_Controller.SetUpCrystal(CrystalDuration, CanExplode, CanMoveToEnemy, MoveSpeed, FindClosetEnemy(CurrentCrystal.transform));

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
                    (CrystalDuration, CanExplode, CanMoveToEnemy, MoveSpeed, FindClosetEnemy(newCrystal.transform));

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
