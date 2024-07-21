using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("Dash")]
    public bool dashUnlocked;
    [SerializeField] public UI_SkillTreeSlot dashUnlockButton;  //ͨ����UI_SkillTreeSlot������ʵ�ּ��ܵĽ���

    [Header("Clone on Dash")]
    public bool cloneOnDashUnlocked;  //��������
    [SerializeField] public UI_SkillTreeSlot cloneOnDashUnlockButton;

    [Header("Clone on Arrival")]
    public bool cloneOnArrivalUnlocked; //δ������
    [SerializeField] public UI_SkillTreeSlot cloneOnArrivalUnlockButton;
    public override void Update()
    {
        base.Update();

        //Debug.Log("Created clone behind");
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }

    public override void Start()
    {
        base.Start();

        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);//GetComponent<Button>().onClick.AddListener �� Unity ������Ϊ��ť��ӵ���¼��������ķ�����ͨ�����ַ�ʽ��������ڰ�ť�����ʱִ��ָ���ĺ�����
        cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
    }


    public void UnlockDash()
    {
        //Debug.Log("Try to Unlock Dash");
        if (dashUnlockButton.unlocked)
        {
            //Debug.Log("dash Unlocked");
            dashUnlocked = true;
            
        }

    }

    public void UnlockCloneOnDash()
    {
        if (cloneOnDashUnlockButton.unlocked)
        { 
            cloneOnDashUnlocked = true;           
        }

    }

    public void UnlockCloneOnArrival()
    { 
        if (cloneOnArrivalUnlockButton.unlocked)
        {
            cloneOnArrivalUnlocked = true;

        }
    }


    public void CreateCloneOnDash()
    {
        if (cloneOnDashUnlocked)
        {
            SkillManager.SkillInstance.Clone.CreateClone(player.transform, Vector3.zero);
        }


    }

    public void CreateCloneOnArrival()
    {
        if (cloneOnArrivalUnlocked)
        {
            SkillManager.SkillInstance.Clone.CreateClone(player.transform, Vector3.zero);
        }
    }
}
