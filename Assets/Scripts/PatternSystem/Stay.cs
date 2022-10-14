using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stay", menuName = "PatternAction/Stay")]
public class Stay : PatternAction {
    
    public float Duration;

    public override void isCollided(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override float PatternDuration => Duration;

    public override void Do(Enemy enemy)
    {
        enemy.Rigidbody.velocity = Vector3.zero;
    }

    public override bool IsFinished(Enemy enemy) {
        return true;
    }
}