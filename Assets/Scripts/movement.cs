using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private float AxisX;
    public float PlayerSpeed;
    private float saveSpeed;
    public float PlayerJumpForce;
    public GameObject AttackBox,Attack2Box,PlayerPivot;
    private bool attack,isJumpPressed;
    public bool isGrounded;
    private Rigidbody rb;
    void Start()
    {
        AttackBox.SetActive(false); Attack2Box.SetActive(false);
        rb = GetComponent<Rigidbody>();
        saveSpeed = PlayerSpeed;
        isJumpPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * PlayerSpeed * AxisX * Time.deltaTime);
        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x, -155, 35),position.y);
        transform.position = position;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 8f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (isJumpPressed)
            {
                rb.AddForce(Vector3.up * PlayerJumpForce,ForceMode.Impulse);
                isJumpPressed = false;
            }
        }
    }

    public void OnMove(InputValue Moving)
    {
        var rotation = transform.rotation;
        AxisX = Moving.Get<float>();
        if (AxisX < 0)
        {
            PlayerPivot.transform.rotation = Quaternion.Euler(rotation.x,180,rotation.z);
        }
        else if(AxisX > 0)
        {
            PlayerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
        }
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            isJumpPressed = true;
        }
    }

    public void OnAttack()
    {
        if (!attack)
        {
            if (isGrounded)
            {
                AttackBox.SetActive(true);
                PlayerSpeed = 0;
                attack = true;
                Invoke("AttackCooldown",0.3f);
            }
            else
            {
                Attack2Box.SetActive(true);
                attack = true;
                Invoke("AttackCooldown",0.3f);
            }
        }
    }

    public void AttackCooldown()
    {
        AttackBox.SetActive(false); Attack2Box.SetActive(false);
        PlayerSpeed = saveSpeed;
        attack = false;
    }
}
