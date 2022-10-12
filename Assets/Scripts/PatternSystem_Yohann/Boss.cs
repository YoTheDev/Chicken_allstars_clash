using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour {

    public List<Pattern_action> bossPattern;
    [HideInInspector] public Rigidbody rb;

    private Pattern_action _currentAction;
    private int _randomActionIndex;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossPattern.Count == 0) return; {
            if (_currentAction == null || _currentAction.IsFinished(this)) {
                if (_currentAction == null) _currentAction = bossPattern.First();
            }
        }
    }

    private Pattern_action GetRandomAction() {
        _randomActionIndex = Random.Range(0, bossPattern.Count);
        return bossPattern[_randomActionIndex];
    }
}
