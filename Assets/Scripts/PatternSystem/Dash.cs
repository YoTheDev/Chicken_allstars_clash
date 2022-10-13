using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dash", menuName = "PatternAction/Dash")]
public class Dash : PatternAction {

    public Vector3 DashPower;
    public float Duration;

    public override float PatternDuration => Duration;

    public override void Do(Enemy enemy) {
        Debug.Log(enemy.name+ " doing " + name);
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(DashPower), ForceMode.Impulse);
    }

    public override bool IsFinished(Enemy enemy) {
        return enemy.Rigidbody.velocity.y <= 0;
    }
}