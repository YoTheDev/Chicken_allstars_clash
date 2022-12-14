using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private float shakeDistance;
        [SerializeField] private GameObject graphics;
        [SerializeField] private GameObject HideBarImage;
        [SerializeField] private GameObject HideBarText;
        [SerializeField] private GameObject HealthBar;
        [SerializeField] private GameObject damageFeedback;
        [SerializeField] private GameObject deathFeedback;
        [SerializeField] private Slider Slider;

        private PatternAction _currentPatternAction;
        private int _currentPatternIndex;
        private int _rngPlayer;
        private float _currentHealth;
        private float _patternTimer;
        private float fixedDeltaTime;
        private float ShakeTimer;
        private float saveSpeed;
        private bool _isDead;
        private bool _enemyReady;
        private bool _playOneShot;
        private Quaternion _rotGoal;
        private Vector3 _direction;
        private Vector2 startPos;
        private Vector2 randomPos;
        private Vector2 healthStartPos;
        private Vector2 healthRandomPos;

        private void Start() {
            HideBarImage.SetActive(false);
            HideBarText.SetActive(false);
            Rigidbody = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0, -180f, 0);
            fixedDeltaTime = Time.fixedDeltaTime;
        }

        public void EnemyStart() {
            _currentHealth = maxHealth;
            Slider.maxValue = maxHealth;
            Slider.value = maxHealth;
            if (!_enemyReady) {
                for (int i = 0; i < target.Count; i++) {
                    if(GameObject.FindWithTag("Player")) target[i] = GameObject.FindWithTag("Player");
                }
                _enemyReady = true;
            }
        }

        private void Update() {
            ShakeTimer += Time.deltaTime;
            if(!_enemyReady) return;
            if (Pattern.Count == 0) { Debug.LogWarning("List for " + gameObject.name + " is set to 0"); return; }
            if (!_isDead && !gameManagement.gameOver) {
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
            if (ShakeTimer < 0.2) {
                startPos = transform.position;
                randomPos = startPos + (Random.insideUnitCircle * shakeDistance);
                graphics.transform.position = randomPos;
            }
            if (_currentHealth <= maxHealth / 3) {
                HideBarImage.SetActive(true);
                HideBarText.SetActive(true);
            }
        }

        public void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player") && !Knockback) {
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
            if (other.gameObject.CompareTag("Attack") || other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("DeathBalloon")) {
                if (other.gameObject.CompareTag("Attack") || other.gameObject.CompareTag("DeathBalloon")) {
                    ShakeTimer = 0;
                    float damage = other.GetComponentInParent<Player_class>()._currentWeapon.DamageData;
                    float score = other.GetComponentInParent<Player_class>()._currentWeapon.ScoreData;
                    Player_management playerManagement =
                        GameObject.Find("Player_manager").GetComponent<Player_management>();
                    playerManagement.scoreEarned += score;
                    Instantiate(damageFeedback, transform.position, transform.rotation);
                    healthStartPos = HealthBar.transform.position;
                    healthRandomPos = healthStartPos + (Random.insideUnitCircle * shakeDistance);
                    HealthBar.transform.position = healthRandomPos;
                    _currentHealth -= damage;
                    Slider.value -= damage;
                }
                if (other.gameObject.CompareTag("Projectile")) {
                    ShakeTimer = 0;
                    float damage = other.GetComponentInParent<projectile>().damage;
                    Instantiate(damageFeedback, transform.position, transform.rotation);
                    _currentHealth -= damage;
                    Slider.value -= damage;
                }
                Debug.Log(_currentHealth);
                if (_currentHealth <= 0) {
                    Instantiate(deathFeedback,transform.position,transform.rotation);
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
