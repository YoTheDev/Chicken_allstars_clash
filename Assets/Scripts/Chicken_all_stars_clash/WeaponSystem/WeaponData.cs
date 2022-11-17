using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject {
    
    public abstract float DamageData { get; }
    public abstract void DoSimple(Player_controll player);
    public abstract void DoAirSimple(Player_controll player);
    public abstract void Interrupt(Player_controll player);
}
