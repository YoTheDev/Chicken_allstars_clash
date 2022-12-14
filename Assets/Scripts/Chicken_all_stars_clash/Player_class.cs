using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PatternSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Player_class : MonoBehaviour {
    
    [SerializeField] private GameObject playerPivot;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float doubleJumpHeight;
    [SerializeField] public float airattackjumpHeight;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackForceUp;
    [SerializeField] private float maxHealth;
    [SerializeField] private float repeatAttackTime;
    [SerializeField] private float repeatAttackNumber;

    private bool _isJumpPressed;
    private bool _playOneShot;
    private float _damage;
    private GameObject _boss;
    public Player_management player_management;
    private Slider _slider01;
    private Slider _slider02;
    
    [HideInInspector] public WeaponData _currentWeapon;
    [HideInInspector] public bool _canAirAttack;
    [HideInInspector] public bool _saveAxisXpositive;
    [HideInInspector] public bool _doubleJump;
    [HideInInspector] public float _axisX;
    [HideInInspector] public float _saveSpeed;
    [HideInInspector] public float nearGroundedRange;
    [HideInInspector] public int weaponIndex;
    [HideInInspector] public int currentPlayerInputIndex;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isNearGrounded;
    [HideInInspector] public bool _isGrounded;
    [HideInInspector] public bool _attack;
    [HideInInspector] public bool _airAttack;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public Collider attackBoxCollider;
    
    public float playerSpeed;
    public float simpleReload;
    public float airReload;
    public float propulsion;
    public GameObject attackBox;
    public GameObject attack2Box;
    public GameObject projectile;
    public GameObject deathBalloon;
    public List<WeaponData> weapon;
    public Game_management Game_management;
    public TextMeshPro PlayerIndicator;
    public List<string> playerLifeUIstring;

    void Start() {
        _saveAxisXpositive = true;
        deathBalloon.SetActive(false);
        player_management = GameObject.Find("Player_manager").GetComponent<Player_management>();
        currentPlayerInputIndex = GetComponent<PlayerInput>().playerIndex;
        Game_management.playerAlive[currentPlayerInputIndex] = true;
        _slider01 = GameObject.Find(playerLifeUIstring[currentPlayerInputIndex]+"/Health_bar_01").GetComponent<Slider>();
        _slider02 = GameObject.Find(playerLifeUIstring[currentPlayerInputIndex]+"/Health_bar_02").GetComponent<Slider>();
        PlayerIndicator.text = "Player " + (currentPlayerInputIndex + 1);
        attackBox.SetActive(false); attack2Box.SetActive(false);
        _boss = GameObject.FindWithTag("Boss");
        _rigidbody = GetComponent<Rigidbody>();
        _saveSpeed = playerSpeed;
        _slider01.maxValue = maxHealth; _slider02.maxValue = maxHealth;
        _slider01.value = maxHealth; _slider02.value = maxHealth;
        if (weapon.Count == 0) {
            Debug.LogWarning("List for " + gameObject.name + " is set to 0");
            return;
        }
        if (_currentWeapon == null) _currentWeapon = weapon[1];
    }
    
    private void FixedUpdate()
    {
        isNearGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), nearGroundedRange);
        if (!isNearGrounded && !isDead) {
            _rigidbody.drag = 3;
            playerSpeed = 50;
        }
        _rigidbody.AddForce(Vector3.left * _axisX * playerSpeed,ForceMode.Force);
        if (_isJumpPressed && _isGrounded) {
            _rigidbody.AddForce(Vector3.up * jumpHeight,ForceMode.Impulse);
            _isGrounded = false;
            _isJumpPressed = false;
            _canAirAttack = true;
            _doubleJump = true;
        }
        if (Game_management.victory && !_playOneShot) {
            player_management.scoreEarned += _slider02.value;
            if (_slider02.value >= maxHealth) {
                player_management.scoreEarned += 1000;
                Debug.Log("No damage bonus");
            }
            _playOneShot = true;
        }
        if (_slider01.value < _slider02.value) _slider02.value -= 0.05f;
        if (isDead) return;
        if (!(_slider02.value <= 0)) return;
        isDead = true;
        playerSpeed = 10;
        CancelInvoke(nameof(InvulnerabilityEnd));
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        Game_management._aliveIndex = currentPlayerInputIndex;
        Invoke(nameof(PlayerBigger),2);
    }

    void PlayerBigger() {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * 100,ForceMode.Impulse);
        Game_management.PlayerDead();
        _rigidbody.mass = 0.10f;
        _currentWeapon = weapon.First();
        playerSpeed = 2;
        _rigidbody.drag = 0.3f;
        playerPivot.SetActive(false);
        deathBalloon.SetActive(true);
        deathBalloon.layer = LayerMask.NameToLayer("IgnoreCollision");
        Invoke(nameof(BalloonCollisionActive),1);
    }
    void BalloonCollisionActive() { deathBalloon.layer = LayerMask.NameToLayer("Default"); }

    public void OnMove(InputValue Moving) {
        if (!player_management.ActivateInput || Game_management.victory || Game_management.gameOver) return;
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
        if (!player_management.ActivateInput || Game_management.victory || isDead) return;
        if (isNearGrounded && !_attack) {
            _isJumpPressed = true;
        }
        if (!_isGrounded && _doubleJump) {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * doubleJumpHeight,ForceMode.Impulse);
            if (_axisX != 0) {
                if (!_saveAxisXpositive) {
                    _rigidbody.AddForce(Vector3.right * propulsion,ForceMode.Impulse);
                }
                else {
                    _rigidbody.AddForce(Vector3.left * propulsion,ForceMode.Impulse);
                }
            }
            _doubleJump = false;
            _isJumpPressed = false;
            _canAirAttack = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            _currentWeapon.currentAirProjectile = 0;
            _isGrounded = true;
            if(!isDead) {
                _rigidbody.drag = 10;
                playerSpeed = _saveSpeed;
                AttackCooldown();
            }
        }
        if (other.gameObject.CompareTag("Boss")) {
            if (isDead) return;
            _rigidbody.velocity = new Vector3(0, 0, 0);
            Vector3 knockbackDirection = new Vector3(transform.position.x - _boss.transform.position.x, 0);
            _rigidbody.AddForce(knockbackDirection * knockbackForce,ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.up * knockbackForceUp,ForceMode.Impulse);
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
            Invoke(nameof(InvulnerabilityEnd), 1);
            AttackCooldown();
            Damage();
        }
        if (isDead) _currentWeapon.Interrupt(this);
    }

    void Damage() {
        _damage = FindObjectOfType<Enemy>().damageCoast;
        if (_slider01.value > 0) _slider01.value -= _damage;
        else _slider02.value -= _damage;
    }

    void InvulnerabilityEnd() {
        gameObject.layer = LayerMask.NameToLayer("Player_one");
    }

    public void OnAttack() {
        if (_attack || !player_management.ActivateInput) return;
        if (isNearGrounded && !_attack && !_airAttack) {
            attackBoxCollider = attackBox.GetComponent<Collider>();
            _currentWeapon.DoSimple(this);
            if (_currentWeapon.SimpleMultipleDamage) {
                attackBox.SetActive(true);
                InvokeRepeating(nameof(DoSimpleAttack), repeatAttackTime, repeatAttackNumber);
            }
            CancelInvoke(nameof(AttackCooldown));
            Invoke(nameof(AttackCooldown),simpleReload);
        }
        else if (_canAirAttack) {
            _currentWeapon.DoAirSimple(this);
            CancelInvoke(nameof(AttackCooldown));
            Invoke(nameof(AttackCooldown),airReload);
        }
    }
    
    void DoSimpleAttack() { _currentWeapon.DoSimple(this); }

    public void AttackCooldown() {
        CancelInvoke(nameof(DoSimpleAttack));
        var rotation = transform.rotation;
        if (!isDead) {
            if (_saveAxisXpositive) playerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
            else playerPivot.transform.rotation = Quaternion.Euler(rotation.x,180,rotation.z);
            playerSpeed = _saveSpeed;
        }
        if(_attack) {
            _attack = false;
            attackBox.SetActive(false);
        }
        if(_airAttack) {
            _airAttack = false;
            attack2Box.SetActive(false);
        }
    }
}
