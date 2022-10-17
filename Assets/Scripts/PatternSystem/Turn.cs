using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turn", menuName = "PatternAction/Turn")]
public class Turn : PatternAction
{
    public Vector3 JumpPower;
    public Transform[] target;
    public float turnSpeed = .01f;
    public float Duration;
    
    private Quaternion _rotGoal;
    private Vector3 _direction;
    private bool _loop;

    public override float PatternDuration => Duration;
    
    public override void Do(Enemy enemy) {
        _loop = true;
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.AddForce(enemy.transform.TransformDirection(JumpPower), ForceMode.Impulse);
        int RNG_player = Random.Range(0, target.Length);
        while (_loop) {
            _direction = (target[RNG_player].position - enemy.transform.position).normalized;
            _rotGoal = Quaternion.LookRotation(_direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,_rotGoal,turnSpeed);
        }
    }

    public override bool IsFinished(Enemy enemy) {
        _loop = false;
        return true;
    }

    public override void isCollided(Enemy enemy) {
        throw new System.NotImplementedException();
    }
}
