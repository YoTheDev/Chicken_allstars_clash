using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Double saber",menuName = "Weapon/Double saber")]
public class DoubleSaber : WeaponData
{
    private float damageGiven;

    public float simpleDamage;
    public float airSimpleDamage;

    public override float DamageData => damageGiven;

    public override void DoSimple(Player_management player) {
        player.attackBox.SetActive(true);
        player.playerSpeed = 0;
        player._attack = true;
        damageGiven = simpleDamage;
    }

    public override void DoAirSimple(Player_management player) {
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
        damageGiven = airSimpleDamage;
    }
}
