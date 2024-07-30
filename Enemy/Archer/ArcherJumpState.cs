using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcherJumpState : EnemyState
{
    public Enemy_Archer archerEenmy;
    public ArcherJumpState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEenmy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.archerEenmy = archerEenmy;
    }

    public override void Enter()
    {
        base.Enter();

        archerEenmy.rb.velocity = new Vector2(archerEenmy.archerJumpVelocity.x * -archerEenmy.facingDir, archerEenmy.archerJumpVelocity.y);
    }
    public override void Update()
    {
        base.Update();

        archerEenmy.anim.SetFloat("yVelocity", archerEenmy.rb.velocity.y);

        if (archerEenmy.rb.velocity.y < 0 && archerEenmy.IsGroundDetected()) //¹­¼ýÊÖÂäµØ
        {
            enemyStateMachine.changeState(archerEenmy.archerBattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
