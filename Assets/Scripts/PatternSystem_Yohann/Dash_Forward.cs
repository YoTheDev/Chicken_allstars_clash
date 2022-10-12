using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dash_Foreward",menuName = "Pattern_action_YS/Dash_Foreward")]
public class Dash_Forward : Pattern_action {
    
    public Vector3 Dash;
    
    public override void ToDo(Boss boss)
    {
        Debug.Log("Im doing");
    }

    public override bool IsFinished(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}
