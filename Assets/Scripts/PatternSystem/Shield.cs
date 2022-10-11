using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shield", menuName = "PatternAction/Shield")]
public class Shield : PatternAction {
    
    public float Duration;

    public override float PatternDuration => Duration;

    public override void Do(Enemy enemy) {
        enemy.LaunchMainAttack();
    }

    public override bool IsFinished(Enemy enemy) {
        return true;
    }
    
}