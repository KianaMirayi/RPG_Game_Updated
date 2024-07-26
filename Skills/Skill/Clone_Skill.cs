using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Clone_Skill : Skill
{
    [Header("����")]
    [SerializeField] private GameObject ClonePreFab;
    [SerializeField] private float CloneDuration;
    [SerializeField] private float attackMultiplier;  //������˺�����



    [Header("�������")]
    [SerializeField] public UI_SkillTreeSlot UnlockCloneAttackButton;
    [SerializeField] private bool CanAttack;
    [SerializeField] private float CloneAttackMultiplier;

    [Header("�����Ի���")]
    [SerializeField] private UI_SkillTreeSlot UnlockAgreesiveCloneButton;
    [SerializeField] private float AggresssiveCloneMultiplier;

    public bool canApplyOnHitEffect;

    //[SerializeField] private bool CreateCloneOnStart;
    //[SerializeField] private bool CreateCloneOnOver;
    //[SerializeField] private bool CanCreateCloneOnCounterAttack;

    [Header("�ٻ�ˮ�����ǻ���")]
    [SerializeField] private UI_SkillTreeSlot UnlockCrystalInsteadCloneButton;
    [SerializeField] public bool CrystalInsteadClone;

    [Header("���ػ���/ˮ��")]
    [SerializeField] private UI_SkillTreeSlot UnlockMultipleButton;
    [SerializeField] private float MultipleCloneAttackMultiplier;
    [SerializeField] private bool CanDuplicateClone;
    [SerializeField] private float PossibilityOfDuplicateClone;


    #region Unlock

    public override void Start()
    {
        base.Start();

        UnlockCloneAttackButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
        UnlockAgreesiveCloneButton.GetComponent<Button>().onClick.AddListener(UnlockAggressiveClone);
        UnlockCrystalInsteadCloneButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInsteadClone);
        UnlockMultipleButton.GetComponent<Button>().onClick.AddListener(UnlockMultiple);
    }

    protected override void CheckLoadedSkillUnlock()
    {
        UnlockCloneAttack();
        UnlockAggressiveClone();
        UnlockCrystalInsteadClone();
        UnlockMultiple();
    }

    public void UnlockCloneAttack()
    {
        if (UnlockCloneAttackButton.unlocked)
        {
            CanAttack = true;
            attackMultiplier = CloneAttackMultiplier;
        }

    }

    public void UnlockAggressiveClone()
    {
        if (UnlockAgreesiveCloneButton.unlocked)
        { 
            canApplyOnHitEffect = true;
            attackMultiplier = AggresssiveCloneMultiplier;
        }
    }

    public void UnlockCrystalInsteadClone()
    {
        if (UnlockCrystalInsteadCloneButton.unlocked)
        { 
            CrystalInsteadClone = true;
            
        }
    }

    public void UnlockMultiple()
    {
        if (UnlockMultipleButton.unlocked)
        { 
            CanDuplicateClone = true;
            attackMultiplier = MultipleCloneAttackMultiplier;
        }
    }

    #endregion
    public void CreateClone(Transform clonePosition,Vector3 _offSet)  //���øú�����Ҫ����Transform���λ�ò���
    {
        if (CrystalInsteadClone)
        {
            SkillManager.SkillInstance.Crystal.CreatCrystal();
            return;
        }

        GameObject newClone = Instantiate(ClonePreFab);  //����Ԥ�����е���Ϸ����ʵ����һ��newCloned��������ʵ�ֻ�ӰЧ��

        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(clonePosition,CloneDuration,CanAttack, _offSet, FindClosetEnemy(newClone.transform),CanDuplicateClone, PossibilityOfDuplicateClone,player,attackMultiplier);  //ʵ�������ewCloned�������Clone_Skill_Controller�ű��е�SetUpClone����
    }


   

    public void CreateCloneWithDelay(Transform _enemyTransform)  // �ڵ����ɹ�ʱ�����Ӱ
    {
        //if (CanCreateCloneOnCounterAttack)
        
        StartCoroutine(CloneDelayCorotine(_enemyTransform, new Vector3(1.2f * player.facingDir, 0)));
        
    }

    private IEnumerator CloneDelayCorotine(Transform transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.2f);

        CreateClone(transform, _offset);
    }

}
