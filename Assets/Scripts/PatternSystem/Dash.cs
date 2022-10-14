using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dash", menuName = "PatternAction/Dash")]
public class Dash : PatternAction {

    public Vector3 DashPower;
    public Vector3 KnockbackPower;
    public float Duration;

    public override void isCollided(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(KnockbackPower), ForceMode.Impulse);
        enemy.Knockback = true;
    }

    public override float PatternDuration => Duration;

    public override void Do(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(DashPower), ForceMode.Impulse);
    }

    public override bool IsFinished(Enemy enemy) {
        return enemy.Rigidbody.velocity.y <= 0;
    }
}