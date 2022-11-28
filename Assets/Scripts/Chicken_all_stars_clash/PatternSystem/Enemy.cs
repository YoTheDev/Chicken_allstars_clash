using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace PatternSystem {
    public class Enemy : MonoBehaviour {

        public bool RandomizePattern;
        public List<PatternAction> Pattern;
        public Game_management gameManagement;
        public List<GameObject> target = new List<GameObject>();
        public float turnSpeed = .01f;
        public float maxHealth;
        
        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public bool Knockback;
        [HideInInspector] public bool Turn;
        [HideInInspector] public GameObject player;
        [HideInInspector] public GameObject Wall;
        [HideInInspector] public float damageCoast;

        [SerializeField] private float deathKnockback;
        [SerializeField] private float deathKnockbackUp;
        [SerializeField] private float deathKnockbackBack;

        private PatternAction _currentPatternAction;
        private int _currentPatternIndex;
        private int _rngPlayer;
        private float _currentHealth;
        private float _patternTimer;
        private float fixedDeltaTime;
        private bool _isDead;
        private bool _enemyReady;
        private Quaternion _rotGoal;
        private Vector3 _direction;

        private void Start() {
            Rigidbody = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0, -180f, 0);
            _currentHealth = maxHealth;
            fixedDeltaTime = Time.fixedDeltaTime;
        }

        public void EnemyStart() {
            if(GameObject.FindWithTag("Player_01")) target.Add(GameObject.FindWithTag("Player_01"));
            if(GameObject.FindWithTag("Player_02")) target.Add(GameObject.FindWithTag("Player_02"));
            if(GameObject.FindWithTag("Player_03")) target.Add(GameObject.FindWithTag("Player_03"));
            if(GameObject.FindWithTag("Player_04")) target.Add(GameObject.FindWithTag("Player_04"));
            _enemyReady = true;
        }

        private void Update() {
            if(!_enemyReady) return;
            if (Pattern.Count == 0) {
                Debug.LogWarning("List for " + gameObject.name + " is set to 0");
                return;
            }
            if (_isDead == false || gameManagement.GameOver()) {
                if (_currentPatternAction == null || _currentPatternAction.IsFinished(this) &&
                    _patternTimer >= _currentPatternAction.PatternDuration) {
                    if (_currentPatternAction == null) _currentPatternAction = Pattern.First();
                    else _currentPatternAction = RandomizePattern ? GetRandomPatternAction() : GetNextPatternAction();
                    _currentPatternAction.Do(this);
                    _patternTimer = 0;
                    damageCoast = _currentPatternAction.PatternDamage;
                }
                _patternTimer += Time.deltaTime;
            }
            if (Turn) {
                Vector3 posTarget = target[_rngPlayer].transform.position ;
                Vector3 posOrigin = transform.position;
                Quaternion rotOrigin = transform.rotation;
                _direction = (posTarget - posOrigin).normalized;
                _direction.y = 0; _direction.z = 0;
                _rotGoal = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(rotOrigin,_rotGoal,turnSpeed * Time.deltaTime);
            }
        }

        public void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player_01") || other.gameObject.CompareTag("Player_02") || other.gameObject.CompareTag("Player_03") || other.gameObject.CompareTag("Player_04")) {
                player = other.gameObject;
                _currentPatternAction.isCollided(this);
            }
            else if (other.gameObject.CompareTag("Wall")) {
                Wall = other.gameObject;
                _currentPatternAction.isCollidedWall(this);
            }
            if(other.gameObject.CompareTag("Ground")) {
                if (Knockback) {
                    Rigidbody.velocity = Vector3.zero;
                    _patternTimer = _currentPatternAction.PatternDuration;
                    Knockback = false;
                }
                if (Turn) transform.rotation = Quaternion.LookRotation(_direction);
                _rngPlayer = Random.Range(0, target.Count);
                Rigidbody.velocity = Vector3.zero;
                Turn = false;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Attack") || other.gameObject.CompareTag("Projectile")) {
                if (other.gameObject.CompareTag("Attack")) {
                    float damage = other.GetComponentInParent<Player_class>()._currentWeapon.DamageData;
                    _currentHealth -= damage;
                }
                if (other.gameObject.CompareTag("Projectile")) {
                    float damage = other.GetComponentInParent<projectile>().damage;
                    _currentHealth -= damage;
                }
                Debug.Log(_currentHealth);
                if (_currentHealth <= 0) {
                    _isDead = true;
                    player = other.gameObject;
                    Vector3 posTarget = player.transform.position;
                    Vector3 posOrigin = transform.position;
                    _direction = (posTarget - posOrigin).normalized;
                    _direction.y = 0; _direction.z = 0;
                    transform.rotation = Quaternion.LookRotation(_direction);
                    Rigidbody.velocity = Vector3.zero;
                    Vector3 knockbackDirection = new Vector3(posOrigin.x - posTarget.x, 0);
                    Rigidbody.AddForce(knockbackDirection * deathKnockback,ForceMode.Impulse);
                    Rigidbody.AddForce(Vector3.up * deathKnockbackUp,ForceMode.Impulse);
                    Rigidbody.AddForce(Vector3.back * deathKnockbackBack,ForceMode.Impulse);
                    gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
                    Time.timeScale = 0.05f;
                    Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
                    Invoke(nameof(NormalizeTime),0.05f);
                }
            }
        }

        void NormalizeTime() {
            Time.timeScale = 1;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            gameManagement.Victory();
        }

        private PatternAction GetRandomPatternAction() {
            _currentPatternIndex = Random.Range(0, Pattern.Count);
            return Pattern[_currentPatternIndex];
        }

        private PatternAction GetNextPatternAction() {
            _currentPatternIndex++;
            if (_currentPatternIndex >= Pattern.Count) _currentPatternIndex = 0;
            return Pattern[_currentPatternIndex];
        }
    }
}
