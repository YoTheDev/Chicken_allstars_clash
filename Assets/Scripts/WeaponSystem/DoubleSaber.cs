using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSaber : WeaponData
{
    public float damageGiven;
    public override float DamageData => damageGiven;
    public override void Do(Player_management player) {
        
    }
}
