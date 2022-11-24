using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Nitro Baker",menuName = "ChickenAllStarsClash/InGame/Weapon/NitroBaker")]
public class NitroBaker : WeaponData {
    private float damageGiven;
    
    public float airSimpleDamage;
    public float saveDamage;

    public override float DamageData => damageGiven;
    public override float currentAirProjectile { get; set; }

    public override void DoSimple(Player_class player) {
        Instantiate(player.projectile,player.transform);
        player.playerSpeed = 0;
        player._attack = true;
    }

    public override void DoAirSimple(Player_class player) {
        saveDamage = airSimpleDamage;
        player.attack2Box.SetActive(true);
        player._doubleJump = false;
        player._rigidbody.velocity = Vector3.zero;
        if (player._axisX != 0) {
            if (!player._saveAxisXpositive) {
                player._rigidbody.AddForce(Vector3.right * 20,ForceMode.Impulse);
            }
            else {
                player._rigidbody.AddForce(Vector3.left * 20,ForceMode.Impulse);
            }
        }
        player._airAttack = true;
        player._canAirAttack = false;
        player._rigidbody.AddForce(Vector3.up * player.airattackjumpHeight,ForceMode.Impulse);
        airSimpleDamage = Random.Range(airSimpleDamage, airSimpleDamage + 3);
        damageGiven = airSimpleDamage;
        airSimpleDamage = saveDamage;
    }

    public override void Interrupt(Player_class player) {
        player.attackBox.SetActive(false); player.attack2Box.SetActive(false);
    }
}
