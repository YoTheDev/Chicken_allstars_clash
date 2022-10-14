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
        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public bool Knockback;

        private PatternAction _currentPatternAction;
        private int _currentPatternIndex;
        private float _patternTimer;

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
                _patternTimer = 0;
            }
            _patternTimer += Time.deltaTime;
        }

        public void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player")) {
                _currentPatternAction.isCollided(this);
            }
            if (Knockback) {
                if (other.gameObject.CompareTag("Ground")) {
                    Rigidbody.velocity = new Vector3(0, 0, 0);
                    _currentPatternAction.IsFinished(this);
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
