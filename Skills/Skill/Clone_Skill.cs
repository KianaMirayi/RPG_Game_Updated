using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject ClonePreFab;
    [SerializeField] private float CloneDuration;
    [SerializeField] private bool CanAttack;

    //[SerializeField] private bool CreateCloneOnStart;
    //[SerializeField] private bool CreateCloneOnOver;
    [SerializeField] private bool CanCreateCloneOnCounterAttack;

    [Header("Crystal instead of Clone")]
    [SerializeField] public bool CrystalInsteadClone;

    [Header("Clone can duplicate")]
    [SerializeField] private bool CanDuplicateClone;
    [SerializeField] private float PossibilityOfDuplicateClone;

    public void CreateClone(Transform clonePosition,Vector3 _offSet)  //���øú�����Ҫ����Transform���λ�ò���
    {
        if (CrystalInsteadClone)
        {
            SkillManager.SkillInstance.Crystal.CreatCrystal();
            return;
        }

        GameObject newClone = Instantiate(ClonePreFab);  //����Ԥ�����е���Ϸ����ʵ����һ��newCloned��������ʵ�ֻ�ӰЧ��

        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(clonePosition,CloneDuration,CanAttack, _offSet, FindClosetEnemy(newClone.transform),CanDuplicateClone, PossibilityOfDuplicateClone,player);  //ʵ�������ewCloned�������Clone_Skill_Controller�ű��е�SetUpClone����
    }


   

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)  // �ڵ����ɹ�ʱ�����Ӱ
    {
        if (CanCreateCloneOnCounterAttack)
        {
            StartCoroutine(CreatCloneWithDelay(_enemyTransform, new Vector3(1.2f * player.facingDir, 0)));
        }
    }

    private IEnumerator CreatCloneWithDelay(Transform transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.2f);

        CreateClone(transform, _offset);
    }

}
