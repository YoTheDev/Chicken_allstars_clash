using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class Player_management : MonoBehaviour {
    
    [SerializeField] private GameObject attackBox;
    [SerializeField] private GameObject attack2Box;
    [SerializeField] private GameObject playerPivot;
    [SerializeField] private bool isNearGrounded,isJumpPressed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float airattackjumpHeight;
    [SerializeField] private float doubleJumpHeight;
    [SerializeField] private float nearGroundedRange;
    
    private float _saveSpeed;
    private float _axisX;
    private bool _isGrounded;
    private bool _attack;
    private bool _airAttack;
    private bool _canAirAttack;
    private bool _saveAxisXpositive;
    private bool _doubleJump;
    private Vector3 _playerVelocity;
    private Rigidbody _rigidbody;

    void Start() {
        attackBox.SetActive(false); attack2Box.SetActive(false);
        _rigidbody = GetComponent<Rigidbody>();
        _saveSpeed = playerSpeed;
        isJumpPressed = false;
    }

    private void FixedUpdate()
    {
        isNearGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), nearGroundedRange);
        if (!_isGrounded) {
            _rigidbody.drag = 3;
            playerSpeed = 50;
        }
        _rigidbody.AddForce(Vector3.left * _axisX * playerSpeed,ForceMode.Force);
        if (isJumpPressed && _isGrounded) {
            _rigidbody.AddForce(Vector3.up * jumpHeight,ForceMode.Impulse);
            _isGrounded = false;
            isJumpPressed = false;
            _canAirAttack = true;
            _doubleJump = true;
        }
    }

    public void OnMove(InputValue Moving) {
        var rotation = transform.rotation;
        _axisX = Moving.Get<float>();
        if (_axisX < 0) {
            if (!_attack && !_airAttack) {
                playerPivot.transform.rotation = Quaternion.Euler(rotation.x,180, rotation.z);
            }
            _saveAxisXpositive = false;
        }
        else if (_axisX > 0) {
            if (!_attack && !_airAttack) {
                playerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
            }
            _saveAxisXpositive = true;
        }
    }

    public void OnJump() {
        if (isNearGrounded && !_attack) {
            isJumpPressed = true;
        }
        if (!_isGrounded && _doubleJump) {
            //Physics.gravity = new Vector3(0, -50f, 0);
            _rigidbody.velocity = Vector3.Normalize(Vector3.up);
            _rigidbody.AddForce(Vector3.up * doubleJumpHeight,ForceMode.Impulse);
            if (_axisX != 0)
            {
                if (!_saveAxisXpositive) {
                    _rigidbody.AddForce(Vector3.right * 20,ForceMode.Impulse);
                }
                else {
                    _rigidbody.AddForce(Vector3.left * 20,ForceMode.Impulse);
                }
            }
            _doubleJump = false;
            isJumpPressed = false;
            _canAirAttack = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            _rigidbody.drag = 10;
            playerSpeed = _saveSpeed;
            _isGrounded = true;
            //Physics.gravity = new Vector3(0, -150f, 0);
        }
    }

    public void OnAttack()
    {
        if (_attack) return;
        if (isNearGrounded) {
            attackBox.SetActive(true);
            playerSpeed = 0;
            _attack = true;
            Invoke("AttackCooldown",0.5f);
        }
        else if (_canAirAttack) {
            _doubleJump = false;
            //Physics.gravity = new Vector3(0, -200f, 0);
            _rigidbody.velocity = Vector3.Normalize(Vector3.up);
            if (_axisX != 0)
            {
                if (!_saveAxisXpositive) {
                    _rigidbody.AddForce(Vector3.right * 20,ForceMode.Impulse);
                }
                else {
                    _rigidbody.AddForce(Vector3.left * 20,ForceMode.Impulse);
                }
            }
            attack2Box.SetActive(true);
            _airAttack = true;
            _canAirAttack = false;
            _rigidbody.AddForce(Vector3.up * airattackjumpHeight,ForceMode.Impulse);
            Invoke("AttackCooldown",0.4f);
        }
    }

    public void AttackCooldown() {
        var rotation = transform.rotation;
        if (_saveAxisXpositive) {
            playerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
        }
        else {
            playerPivot.transform.rotation = Quaternion.Euler(rotation.x,180,rotation.z);
        }
        attackBox.SetActive(false); attack2Box.SetActive(false);
        playerSpeed = _saveSpeed;
        _attack = false; _airAttack = false;
    }
}
