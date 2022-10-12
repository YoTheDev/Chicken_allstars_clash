using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern_action : ScriptableObject {
    
    public abstract void ToDo(Boss boss);
    public abstract bool IsFinished(Boss boss);
}
