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
    public GameObject AttackBox;
    private bool attack;
    private Rigidbody rb;
    void Start()
    {
        AttackBox.SetActive(false);
        rb = GetComponent<Rigidbody>();
        saveSpeed = PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * PlayerSpeed * AxisX * Time.deltaTime);
        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x, -155, 35),position.y);
        transform.position = position;
    }

    public void OnMove(InputValue Moving)
    {
        var rotation = transform.rotation;
        AxisX = Moving.Get<float>();
        if (AxisX < 0)
        {
            transform.Rotate(rotation.x,180,rotation.z);
        }
        else if(AxisX > 0)
        {
            transform.Rotate(rotation.x,180,rotation.z);
        }
    }

    public void OnJump()
    {
        rb.AddForce(Vector3.up * PlayerJumpForce,ForceMode.Impulse);
    }

    public void OnAttack()
    {
        if (!attack)
        {
            AttackBox.SetActive(true);
            PlayerSpeed = 0;
            attack = true;
            Invoke("AttackCooldown",0.3f);
        }
    }

    public void AttackCooldown()
    {
        AttackBox.SetActive(false);
        PlayerSpeed = saveSpeed;
        attack = false;
    }
}
