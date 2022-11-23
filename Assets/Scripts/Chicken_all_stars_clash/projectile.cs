using System;
using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class projectile : MonoBehaviour
{
    [SerializeField] private bool isExplode;
    [SerializeField] private bool isExplosion;
    [SerializeField] private bool savantPotion;
    [SerializeField] private bool mageStick;
    [SerializeField] private int explosionTime;
    [SerializeField] private GameObject explosion;

    public Rigidbody rb;
    public Player_class playerScript;

    private void Start() {
        if (isExplode) explosion.SetActive(false);
        playerScript = GetComponentInParent<Player_class>();
        GameObject player = playerScript.gameObject;
        var transform1 = transform;
        transform1.position = player.transform.position;
        transform1.parent = null;
        if (savantPotion) {
            rb.AddForce(Vector3.up * 60,ForceMode.Impulse);
            rb.AddForce(Vector3.left * 20,ForceMode.Impulse);
        }
        if (mageStick) {
            rb.AddForce(Vector3.down * 100,ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("DeathZone")) Destroy(gameObject);
        if (other.gameObject.CompareTag("Ground") && isExplode) {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            explosion.SetActive(true);
            Invoke(nameof(ExplosionEnd),explosionTime);
        }
    }
    void ExplosionEnd() { 
        explosion.SetActive(false);
        Destroy(gameObject); 
    }
}
