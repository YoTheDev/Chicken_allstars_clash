using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject {
    
    public abstract float DamageData { get; }
    public abstract void Do(Player_management player);
}
