using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
   public EnemyState enemyCurrentState { get; private set; }

    public void Initialize(EnemyState startState)
    {
        enemyCurrentState = startState;
        enemyCurrentState.Enter();
    }

    public void changeState(EnemyState newState)
    {
        enemyCurrentState.Exit();
        enemyCurrentState = newState;
        enemyCurrentState.Enter();
    }

}
