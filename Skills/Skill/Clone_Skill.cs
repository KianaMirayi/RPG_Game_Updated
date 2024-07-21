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

    public void CreateClone(Transform clonePosition,Vector3 _offSet)  //调用该函数需要传入Transform类的位置参数
    {
        if (CrystalInsteadClone)
        {
            SkillManager.SkillInstance.Crystal.CreatCrystal();
            return;
        }

        GameObject newClone = Instantiate(ClonePreFab);  //利用预制体中的游戏对象实例化一个newCloned对象，用于实现幻影效果

        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(clonePosition,CloneDuration,CanAttack, _offSet, FindClosetEnemy(newClone.transform),CanDuplicateClone, PossibilityOfDuplicateClone,player);  //实例化后的ewCloned对象调用Clone_Skill_Controller脚本中的SetUpClone方法
    }


   

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)  // 在弹反成功时创造幻影
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
