using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PatternSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Player_management : MonoBehaviour {
    
    
    [SerializeField] private GameObject playerPivot;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float doubleJumpHeight;
    [SerializeField] public float airattackjumpHeight;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackForceUp;
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider slider01;
    [SerializeField] private Slider slider02;
    
    private bool isJumpPressed;
    private float _damage;
    private GameObject _boss;
    
    [HideInInspector] public WeaponData _currentWeapon;
    [HideInInspector] public bool _canAirAttack;
    [HideInInspector] public bool _saveAxisXpositive;
    [HideInInspector] public bool _doubleJump;
    [HideInInspector] public float _axisX;
    [HideInInspector] public float _saveSpeed;
    [HideInInspector] public float nearGroundedRange;
    [HideInInspector] public int weaponIndex;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isNearGrounded;
    [HideInInspector] public bool _isGrounded;
    [HideInInspector] public bool _attack;
    [HideInInspector] public bool _airAttack;
    [HideInInspector] public Rigidbody _rigidbody;
    
    public float playerSpeed;
    public GameObject attackBox;
    public GameObject attack2Box;
    public List<WeaponData> weapon;
    public Game_management Game_management;

    void Start() {
        attackBox.SetActive(false); attack2Box.SetActive(false);
        _boss = GameObject.FindWithTag("Boss");
        _rigidbody = GetComponent<Rigidbody>();
        _saveSpeed = playerSpeed;
        slider01.maxValue = maxHealth; slider02.maxValue = maxHealth;
        if (weapon.Count == 0) {
            Debug.LogWarning("List for " + gameObject.name + " is set to 0");
            return;
        }
        if (_currentWeapon == null) _currentWeapon = weapon.First();
        Game_management.PlayerCount();
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
                CancelInvoke(nameof(InvulnerabilityEnd));
                gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
                Game_management.PlayerDead();
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
            _currentWeapon.DoSimple(this);
            Invoke(nameof(AttackCooldown),0.4f);
        }
        else if (_canAirAttack) {
            _currentWeapon.DoAirSimple(this);
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
