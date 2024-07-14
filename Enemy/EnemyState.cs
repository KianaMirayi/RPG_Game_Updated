using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine enemyStateMachine { get; private set;  }
    
    protected Enemy enemyBase { get; private set; }

    protected string enemyAnimBoolName { get; private set; }


    public float stateTimer;

    protected bool Triggercalled;

    public EnemyState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName)
    {
        this.enemyBase = enemyBase;
        this.enemyStateMachine = enemyStateMachine;
        this.enemyAnimBoolName = enemyAnimBoolName;
    }

    public virtual void Enter()
    {
        Triggercalled = false;

        enemyBase.anim.SetBool(enemyAnimBoolName, true);

        
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(enemyAnimBoolName, false);
        enemyBase.AssignLastAnimName(enemyAnimBoolName);
    }


    public virtual void AnimationFinishTrigger()
    {
        Triggercalled = true;  
    }


}
