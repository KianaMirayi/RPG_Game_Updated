using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_DeathBringer : Enemy
{


    public bool bossFightBegun;


    [Header("传送设置")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

    [Header("施法")]
    [SerializeField] private GameObject spellPrefab;
    public int spellCastAmount;
    public float spellCoolDown;
    public float lastTimepellCast;
    [SerializeField]private float SpellCastStateCoolDown;


    public DeathBringerBattleState battleState { get; private set; }
    public DeathBringerAttackState attackState { get; private set; }
    public DeathBringerIdleState idleState { get; private set; }
    public DeathBringerDeadState deadState { get; private set; }
    public DeathBringerSpellCastState spellCastState { get; private set; }
    public DeathBringerTeleportState teleportState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new DeathBringerIdleState(this, stateMachine, "Idle", this);

        battleState = new DeathBringerBattleState(this, stateMachine, "Move", this);

        attackState = new DeathBringerAttackState(this, stateMachine, "Attack", this);

        deadState = new DeathBringerDeadState(this, stateMachine, "Idle", this);

        spellCastState = new DeathBringerSpellCastState(this, stateMachine, "SpellCast",this);

        teleportState = new DeathBringerTeleportState(this, stateMachine, "Teleport", this);

    }

    public override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public override void Die()
    {
        base.Die();

        stateMachine.changeState(deadState);
    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;
        float xOffset = 0;

        if (player.rb.velocity.x != 0)
        {
            xOffset = player.facingDir * 3;
        }
        
        Vector3 spllePosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + 3f);

        int amountsInOnce = Random.Range(1,4);

        //for (int a = 0; a < amountsInOnce; a++)
        //{
        //    GameObject newSpell = Instantiate(spellPrefab, spllePosition, Quaternion.identity);
        //    newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
        //}

        GameObject newSpell = Instantiate(spellPrefab, spllePosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);



    }

    public void FindPosition() //boss传送至战场的随机位置
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow())
        {
            Debug.Log("Looking for new position g");
            FindPosition();
        }

        //if (SomethingIsAround()) //加上这条检查内存就溢出了，并且造成卡顿
        //{
        //    Debug.Log("Looking for new position around");
        //    FindPosition();
        //}
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, whatIsGround);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    public bool CanTeleport()//设置boss可以传送的概率
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        { 
            chanceToTeleport = defaultChanceToTeleport; //boss每一次成功传送之后将可传送的概率返回至默认值
            return true;
        }

        return false;
    }

    public bool CanSpellCast()
    {
        if (Time.time >= lastTimepellCast + SpellCastStateCoolDown)
        { 

            
            return true;
        }
        return false;
    }

}
