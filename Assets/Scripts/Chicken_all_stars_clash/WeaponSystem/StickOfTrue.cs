using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StickOfTrue",menuName = "ChickenAllStarsClash/InGame/Weapon/StickOfTrue")]
public class StickOfTrue : WeaponData
{
    private float damageGiven;
    private float scoreGiven;
    
    public float simpleDamage;
    public float airSimpleDamage;
    public float simpleScore;
    public float airSimpleScore;
    public float airProjectileCount;
    public float saveDamage;
    public bool doMultipleDamage;

    public override float DamageData => damageGiven;
    public override float ScoreData => scoreGiven;
    public override float currentAirProjectile { get; set; }

    public override void DoSimple(Player_class player) {
        scoreGiven = simpleScore;
        saveDamage = simpleDamage;
        player.attackBoxCollider.enabled = false;
        player.attackBoxCollider.enabled = true;
        player.playerSpeed = 50;
        player._attack = true;
        simpleDamage = Random.Range(simpleDamage, simpleDamage + 3);
        damageGiven = simpleDamage;
        simpleDamage = saveDamage;
    }

    public override void DoAirSimple(Player_class player) {
        Instantiate(player.projectile,player.transform);
        player._doubleJump = false;
        player._rigidbody.velocity = Vector3.zero;
        if (player._axisX != 0) {
            if (!player._saveAxisXpositive) {
                player._rigidbody.AddForce(Vector3.right * 5,ForceMode.Impulse);
            }
            else {
                player._rigidbody.AddForce(Vector3.left * 5,ForceMode.Impulse);
            }
        }
        player._airAttack = true;
        player._attack = false;
        player._rigidbody.AddForce(Vector3.up * player.airattackjumpHeight, ForceMode.Impulse);
        scoreGiven = airSimpleScore;
        damageGiven = airSimpleDamage;
        currentAirProjectile++;
        if (currentAirProjectile >= airProjectileCount) player._canAirAttack = false;
    }

    public override bool SimpleMultipleDamage => doMultipleDamage;

    public override void Interrupt(Player_class player) {
        player.attackBox.SetActive(false);
    }
}
