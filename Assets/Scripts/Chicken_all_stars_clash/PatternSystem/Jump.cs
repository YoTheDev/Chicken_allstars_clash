using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jump", menuName = "ChickenAllStarsClash/InGame/PatternAction/Jump")]
public class Jump : PatternAction
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public override void Do(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsFinished(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void isCollided(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void isCollidedWall(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override float PatternDuration { get; }
    public override float PatternDamage { get; }
}
