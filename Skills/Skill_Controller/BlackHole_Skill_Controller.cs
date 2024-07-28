using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class BlackHole_Skill_Controller : MonoBehaviour
{
    // 重大BUG：当设置的剑类型为Bounce Spin时，释放黑洞技能将使角色落下并仍处于透明状态

    public SpriteRenderer BlackHoleSr;

    public float MaxSize;
    public float MinSize;
    public float GrowSpeed;
    public bool CanCreatHotKey = true;

    public bool CanGrow = true;
    public bool CanShrink;


    private List<Transform> EnemyTarget = new List<Transform>();
    private List<GameObject> CreateHotKey = new List<GameObject>();

    [SerializeField] public GameObject HotKeyPrefab;
    [SerializeField] public List<KeyCode> KeyCodeList;

    public int CountOfAttacks;
    public float CloneAttackCoolDown = 0.3f;
    public float CloneAttackTimer;
    public bool CanAttack;
    public bool PlayerCanExitState;

    public float BlackHoleDuration;

    public float BlackHoleTimer;

    public void SetUpBlackHole(float maxSize, float minSize, float growSpeed, bool canGrow, int countOfAttacks, float cloneAttackCoolDown, float _blackHoleDuration )
    {
        MaxSize = maxSize;
        MinSize = minSize;
        GrowSpeed = growSpeed;
        CanGrow = canGrow;
        CountOfAttacks = countOfAttacks;
        CloneAttackCoolDown = cloneAttackCoolDown;
        BlackHoleTimer = _blackHoleDuration;

        if (SkillManager.SkillInstance.Clone.CrystalInsteadClone)
        {
            PlayerManager.instance.player.sr.color = Color.white;
        }

    }

    private void Start()
    {
        BlackHoleSr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CloneAttackTimer -= Time.deltaTime;
        BlackHoleTimer -= Time.deltaTime;

        if (BlackHoleTimer < 0)
        {
            BlackHoleTimer = Mathf.Infinity;
            if (EnemyTarget.Count > 0)
            {
                CanAttack = true;
                DestoryHotKey();
                CanCreatHotKey = false;
                
            }
            else
            { 
                PlayerCanExitState = true;
                CanShrink = true;
                CanGrow = false;
                CanAttack = false;
                PlayerManager.instance.player.ExitBlackHoleSkill();
                PlayerManager.instance.player.fx.MakeTransparent(false);
                DestoryHotKey();

            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (EnemyTarget.Count <= 0)
            {
                return;
            }
            CanAttack = true;
            DestoryHotKey();
            CanCreatHotKey = false;
            PlayerManager.instance.player.fx.MakeTransparent(true);
            

        }

        BlackHole_CloneAttackLogic();

        if (CanGrow && !CanShrink)
        {
            transform.localScale = UnityEngine.Vector2.Lerp(transform.localScale, new UnityEngine.Vector2(MaxSize, MaxSize), GrowSpeed * Time.deltaTime);
            PlayerManager.instance.player.fx.MakeTransparent(true);

        }
        if (CanShrink)
        {
            CanGrow = false;
            transform.localScale = UnityEngine.Vector2.Lerp(transform.localScale, new UnityEngine.Vector2(MinSize, MinSize), GrowSpeed * Time.deltaTime * 2);

            if (transform.localScale.x < 0)
            {
                DisableCanGrow();
                Destroy(gameObject);
            }

        }

        //if (SkillManager.SkillInstance.Sword.IsSpin && CanGrow)
        //{
        //    StartCoroutine("ReturnTheSwordToPlayer", 4.5);
        //}



    }

    private void BlackHole_CloneAttackLogic()
    {
        if (CloneAttackTimer < 0 && CanAttack && CountOfAttacks > 0)
        {
            CloneAttackTimer = CloneAttackCoolDown;


            int randomIndex = Random.Range(0, EnemyTarget.Count);
            float xOffSet;

            if (Random.Range(0, 100) > 50)
            {
                xOffSet = 1.5f;
            }
            else
            {
                xOffSet = -1.5f;
            }

            if (SkillManager.SkillInstance.Clone.CrystalInsteadClone)
            {
                SkillManager.SkillInstance.Crystal.CreatCrystal();
                SkillManager.SkillInstance.Crystal.CurrentCrystalChooseRandomTarget();
            }
            else
            { 
                
            }

            SkillManager.SkillInstance.Clone.CreateClone(EnemyTarget[randomIndex], new UnityEngine.Vector3(xOffSet, 0));

            CountOfAttacks--;

            if (CountOfAttacks <= 0)
            {
                PlayerCanExitState = true;
                CanShrink = true;
                CanGrow = false;
                CanAttack = false;
                PlayerManager.instance.player.ExitBlackHoleSkill();
            }
        }
    }

    public void DisableCanGrow()
    {
        CanGrow= false;
    }

    public void DestoryHotKey()
    {
        if (CreateHotKey.Count <= 0)
        { 
            //EnemyTarget.Clear();
            return;
        }

        for (int i = 0; i < CreateHotKey.Count; i++)
        {
            Destroy(CreateHotKey[i]);

        }
    }
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {

            collision.GetComponent<Enemy>().FreezeEnemy(true);

            //float freezeMoveSpeed = 0;
            //collision.GetComponent<Enemy>().anim.speed = 0;
            //collision.GetComponent<Enemy_Skeleton>().moveSpeed = freezeMoveSpeed;

            CreatHotKey(collision);

            //EnemyTarget.Add(collision.transform);
        }


    }

    //public void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeEnemy(false);
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        { 
            collision.GetComponent<Enemy>().FreezeEnemy(false);
            //collision.GetComponent<Enemy_Skeleton>().anim.speed = 1;
            //collision.GetComponent<Enemy_Skeleton>().moveSpeed = 1.5f;
        }
    }

    private void CreatHotKey(Collider2D collision)
    {
        if (KeyCodeList.Count <= 0)
        {
            Debug.Log("No enough hotkeys in KeyCode list");
            return;
        }

        if (!CanCreatHotKey)
        { 
            return;
        }


        GameObject newHotKey = Instantiate(HotKeyPrefab, collision.transform.position + new UnityEngine.Vector3(0, 2), UnityEngine.Quaternion.identity);

        CreateHotKey.Add(newHotKey);

        KeyCode choosenKey = KeyCodeList[Random.Range(0, KeyCodeList.Count)];

        KeyCodeList.Remove(choosenKey);

        BlackHole_HotKeyHint_Controller blackHole_HotKeyHint_Controller = newHotKey.GetComponent<BlackHole_HotKeyHint_Controller>();

        blackHole_HotKeyHint_Controller.SetUpHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemytransform) => EnemyTarget.Add(_enemytransform);





}