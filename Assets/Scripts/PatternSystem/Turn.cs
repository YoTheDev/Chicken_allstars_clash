using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turn", menuName = "PatternAction/Turn")]
public class Turn : PatternAction
{
    public Vector3 JumpPower;
    public float knockbackForce;
    public float knockbackForceUp;
    public float Duration;
    public float damage;

    private Vector3 knockbackDirection;

    public override void isCollidedWall(Enemy enemy) {
        return;
    }

    public override float PatternDuration => Duration;
    public override float PatternDamage => damage;

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
        knockbackDirection = new Vector3(enemy.transform.position.x - enemy.player.transform.position.x, 0);
        enemy.Rigidbody.AddForce(knockbackDirection * knockbackForce,ForceMode.Impulse);
        enemy.Rigidbody.AddForce(Vector3.up * knockbackForceUp,ForceMode.Impulse);
        enemy.Knockback = true;
    }
}
