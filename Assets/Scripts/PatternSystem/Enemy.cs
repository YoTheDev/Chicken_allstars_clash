using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PatternSystem {
    public class Enemy : MonoBehaviour {

        public bool RandomizePattern;
        public List<PatternAction> Pattern;
        [HideInInspector] public bool isCollided;
        [HideInInspector] public Rigidbody Rigidbody;

        private PatternAction _currentPatternAction;
        private int _currentPatternIndex;
        private float _patternTimer;

        private void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
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

        public void OnCollisionEnter(Collision other)
        {
            isCollided = true;
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
