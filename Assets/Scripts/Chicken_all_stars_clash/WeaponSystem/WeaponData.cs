using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject {
    
    public abstract float DamageData { get; }
    public abstract float currentAirProjectile { get; set; }
    public abstract void DoSimple(Player_class player);
    public abstract void DoAirSimple(Player_class player);
    public abstract void Interrupt(Player_class player);
}
