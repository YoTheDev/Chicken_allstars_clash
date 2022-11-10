using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_select : MonoBehaviour
{
    public float xAxis;
    void OnMove(InputValue Moving) {
        xAxis = Moving.Get<float>();
        if (xAxis <= -1) {
            Debug.Log("Left");
        }

        if (xAxis >= 1) {
            Debug.Log("right");
        }
    }
}
