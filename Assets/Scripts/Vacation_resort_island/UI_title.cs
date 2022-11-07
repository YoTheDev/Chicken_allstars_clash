using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_title : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject optionScreen;
    [SerializeField] private GameObject playerSelectScreen;

    private bool _attackPressed;
    private bool _jumpPressed;

    void OnAttackHold() { _attackPressed = !_attackPressed; }
    void OnJumpHold() { _jumpPressed = !_jumpPressed; }
    
    private void Start() {
        titleScreen.SetActive(true);
        startScreen.SetActive(false); optionScreen.SetActive(false); playerSelectScreen.SetActive(false);
    }

    private void Update() {
        if (titleScreen.activeSelf && _jumpPressed && _attackPressed) {
            titleScreen.SetActive(false);
            startScreen.SetActive(true);
        }
    }
}
