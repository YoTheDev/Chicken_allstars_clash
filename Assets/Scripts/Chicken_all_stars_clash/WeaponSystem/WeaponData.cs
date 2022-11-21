using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject {
    
    public abstract float DamageData { get; }
    public abstract float currentAirProjectile { get; set; }
    public abstract void DoSimple(Player_controll player);
    public abstract void DoAirSimple(Player_controll player);
    public abstract void Interrupt(Player_controll player);
}
