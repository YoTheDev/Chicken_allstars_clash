using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turn", menuName = "PatternAction/Turn")]
public class Turn : PatternAction
{
    public Vector3 JumpPower;
    public Vector3 KnockbackPower;
    public float Duration;

    public override float PatternDuration => Duration;
    
    public override void Do(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(JumpPower), ForceMode.Impulse);
        enemy.Turn = true;
    }

    public override bool IsFinished(Enemy enemy) {
        return enemy.Rigidbody.velocity.y <= 0;
    }

    public override void isCollided(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(KnockbackPower), ForceMode.Impulse);
        enemy.Knockback = true;
    }
}
