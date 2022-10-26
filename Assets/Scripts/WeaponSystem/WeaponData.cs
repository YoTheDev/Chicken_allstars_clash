using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject {
    
    public abstract float DamageData { get; }
    public abstract void DoSimple(Player_management player);
    public abstract void DoAirSimple(Player_management player);
}
