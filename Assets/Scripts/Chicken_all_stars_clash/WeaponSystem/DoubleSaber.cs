using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Double saber",menuName = "ChickenAllStarsClash/InGame/Weapon/Double saber")]
public class DoubleSaber : WeaponData {
    private float damageGiven;
    private float scoreGiven;

    public float simpleDamage;
    public float airSimpleDamage;
    public float simpleScore;
    public float airSimpleScore;
    public float saveDamage;
    public bool doMultipleDamage;

    public override float DamageData => damageGiven;
    public override float ScoreData => scoreGiven;
    public override float currentAirProjectile { get; set; }

    public override void DoSimple(Player_class player) {
        scoreGiven = simpleScore;
        saveDamage = simpleDamage;
        player.attackBox.SetActive(true);
        player.playerSpeed = 0;
        player._attack = true;
        simpleDamage = Random.Range(simpleDamage, simpleDamage + 3);
        damageGiven = simpleDamage;
        simpleDamage = saveDamage;
    }

    public override void DoAirSimple(Player_class player) {
        saveDamage = airSimpleDamage;
        player.attack2Box.SetActive(true);
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
        player._canAirAttack = false;
        player._rigidbody.AddForce(Vector3.up * player.airattackjumpHeight,ForceMode.Impulse);
        airSimpleDamage = Random.Range(airSimpleDamage, airSimpleDamage + 3);
        scoreGiven = airSimpleScore;
        damageGiven = airSimpleDamage;
        airSimpleDamage = saveDamage;
    }

    public override bool SimpleMultipleDamage => doMultipleDamage;

    public override void Interrupt(Player_class player) {
        player.attackBox.SetActive(false); player.attack2Box.SetActive(false);
    }
}
