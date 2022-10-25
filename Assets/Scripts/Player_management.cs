using System;
using System.Diagnostics;
using PatternSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackForceUp;
    [SerializeField] public float maxHealth;
    [SerializeField] public Slider slider01;
    [SerializeField] public Slider slider02;
    
    private float _saveSpeed;
    private float _axisX;
    private float _damage;
    private bool _isGrounded;
    private bool _attack;
    private bool _airAttack;
    private bool _canAirAttack;
    private bool _saveAxisXpositive;
    private bool _doubleJump;
    private GameObject _boss;

    public bool isDead;
    public Rigidbody _rigidbody;

    void Start() {
        attackBox.SetActive(false); attack2Box.SetActive(false);
        _boss = GameObject.FindWithTag("Boss");
        _rigidbody = GetComponent<Rigidbody>();
        _saveSpeed = playerSpeed;
        isJumpPressed = false;
        slider01.maxValue = maxHealth; slider02.maxValue = maxHealth;
    }

    private void FixedUpdate()
    {
        isNearGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), nearGroundedRange);
        if (!isNearGrounded) {
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
        if (slider01.value < slider02.value) {
            slider02.value = slider02.value - 0.05f;
        }
        if (isDead == false) {
            if (slider02.value <= 0) {
                isDead = true;
                _axisX = 0;
                playerSpeed = 0;
            }
        }
    }

    public void OnMove(InputValue Moving) {
        if (isDead == false) {
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
    }

    public void OnJump() {
        if (isDead == false) {
            if (isNearGrounded && !_attack) {
                isJumpPressed = true;
            }
            if (!_isGrounded && _doubleJump) {
                _rigidbody.velocity = new Vector3(0, 0, 0);
                _rigidbody.AddForce(Vector3.up * doubleJumpHeight,ForceMode.Impulse);
                if (_axisX != 0) {
                    if (!_saveAxisXpositive) {
                        _rigidbody.AddForce(Vector3.right * 5,ForceMode.Impulse);
                    }
                    else {
                        _rigidbody.AddForce(Vector3.left * 5,ForceMode.Impulse);
                    }
                }
                _doubleJump = false;
                isJumpPressed = false;
                _canAirAttack = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            _rigidbody.drag = 10;
            playerSpeed = _saveSpeed;
            _isGrounded = true;
        }
        if (other.gameObject.CompareTag("Boss")) {
            _rigidbody.velocity = new Vector3(0, 0, 0);
            Vector3 knockbackDirection = new Vector3(transform.position.x - _boss.transform.position.x, 0);
            _rigidbody.AddForce(knockbackDirection * knockbackForce,ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.up * knockbackForceUp,ForceMode.Impulse);
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
            Invoke(nameof(InvulnerabilityEnd),1);
            Damage();
        }
    }

    void Damage() {
        _damage = FindObjectOfType<Enemy>().damageCoast;
        if (slider01.value > 0) {
            slider01.value = slider01.value - _damage;
        }
        else {
            slider02.value = slider02.value - _damage;
        }
    }

    void InvulnerabilityEnd() {
        gameObject.layer = LayerMask.NameToLayer("Player_one");
    }

    public void OnAttack()
    {
        if (_attack) return;
        if (isNearGrounded) {
            attackBox.SetActive(true);
            playerSpeed = 0;
            _attack = true;
            Invoke(nameof(AttackCooldown),0.5f);
        }
        else if (_canAirAttack) {
            _doubleJump = false;
            _rigidbody.velocity = new Vector3(0, 0, 0);
            if (_axisX != 0) {
                if (!_saveAxisXpositive) {
                    _rigidbody.AddForce(Vector3.right * 5,ForceMode.Impulse);
                }
                else {
                    _rigidbody.AddForce(Vector3.left * 5,ForceMode.Impulse);
                }
            }
            attack2Box.SetActive(true);
            _airAttack = true;
            _canAirAttack = false;
            _rigidbody.AddForce(Vector3.up * airattackjumpHeight,ForceMode.Impulse);
            Invoke(nameof(AttackCooldown),0.4f);
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
