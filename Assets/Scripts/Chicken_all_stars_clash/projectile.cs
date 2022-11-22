using System;
using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;

public class projectile : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.down * 100 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ground")) Destroy(gameObject);
    }
}
