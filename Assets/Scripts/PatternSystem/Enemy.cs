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
        public Transform[] target;
        public float turnSpeed = .01f;
        
        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public bool Knockback;
        [HideInInspector] public bool Turn;

        private PatternAction _currentPatternAction;
        private int _currentPatternIndex;
        private int _rngPlayer;
        private float _patternTimer;
        private Quaternion _rotGoal;
        private Vector3 _direction;

        private void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0, -180f, 0);
        }

        private void Update() {
            if (Pattern.Count == 0) return;
            if (_currentPatternAction == null || _currentPatternAction.IsFinished(this) &&
                _patternTimer >= _currentPatternAction.PatternDuration) {
                if (_currentPatternAction == null) _currentPatternAction = Pattern.First();
                else _currentPatternAction = RandomizePattern ? GetRandomPatternAction() : GetNextPatternAction();
                _currentPatternAction.Do(this);
                _rngPlayer = Random.Range(0, target.Length);
                _patternTimer = 0;
            }
            _patternTimer += Time.deltaTime;
            if (Turn) {
                Vector3 posTarget = target[_rngPlayer].position ;
                Vector3 posOrigin = transform.position;
                Quaternion rotOrigin = transform.rotation;
                //Debug.DrawLine(posTarget,posOrigin, Color.green);
                //Vector3 oriantation;
                //float debugDistance = 3;
                //oriantation = posTarget - posOrigin;
                //Debug.DrawLine(posOrigin,posOrigin+(oriantation.normalized * debugDistance), Color.red);
                //transform.right = oriantation;
                _direction = (posTarget - posOrigin).normalized;
                _rotGoal = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(rotOrigin,_rotGoal,turnSpeed);
            }
        }

        public void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall")) {
                _currentPatternAction.isCollided(this);
            }
            if (Knockback) {
                if (other.gameObject.CompareTag("Ground")) {
                    Rigidbody.velocity = new Vector3(0, 0, 0);
                    _patternTimer = _currentPatternAction.PatternDuration;
                    Knockback = false;
                }
            }
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
