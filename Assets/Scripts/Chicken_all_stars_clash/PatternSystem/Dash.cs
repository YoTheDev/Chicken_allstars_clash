using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dash", menuName = "ChickenAllStarsClash/InGame/PatternAction/Dash")]
public class Dash : PatternAction {

    public Vector3 DashPower;
    public float knockbackForce;
    public float knockbackForceUp;
    public float Duration;
    public float damage;
    
    private Vector3 knockbackDirection;
    private Vector3 knockbackDirectionWall;

    public override void isCollided(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        knockbackDirection = new Vector3(enemy.transform.position.x - enemy.player.transform.position.x, 0);
        enemy.Rigidbody.AddForce(knockbackDirection * knockbackForce,ForceMode.Impulse);
        enemy.Rigidbody.AddForce(Vector3.up * knockbackForceUp,ForceMode.Impulse);
        enemy.Knockback = true;
    }

    public override void isCollidedWall(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        knockbackDirectionWall = new Vector3(enemy.transform.position.x - enemy.Wall.transform.position.x, 0);
        enemy.Rigidbody.AddForce(knockbackDirectionWall * knockbackForce,ForceMode.Impulse);
        enemy.Rigidbody.AddForce(Vector3.up * knockbackForceUp,ForceMode.Impulse);
        enemy.Knockback = true;
    }

    public override float PatternDuration => Duration;
    public override float PatternDamage => damage;

    public override void Do(Enemy enemy) {
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(DashPower), ForceMode.Impulse);
    }

    public override bool IsFinished(Enemy enemy) {
        return enemy.Rigidbody.velocity.y <= 0;
    }
}