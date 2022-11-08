using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class UI_title : MonoBehaviour {
    [SerializeField] private GameObject titleScreen;

    public List<GameObject> ui = new List<GameObject>();

    private bool _attackPressed;
    private bool _jumpPressed;
    private int _uiActiveIndex;

    void OnAttackHold() { _attackPressed = !_attackPressed; }
    void OnJumpHold() { _jumpPressed = !_jumpPressed; }
    
    private void Start() {
        titleScreen.SetActive(true);
        for (int i = 0; i < ui.Count; i++) {
            ui[i].SetActive(false);
        }
    }

    private void Update() {
        if (titleScreen.activeSelf && _jumpPressed && _attackPressed) {
            titleScreen.SetActive(false);
            ui[0].SetActive(true);
        }
    }

    public void MorePlayer() {
        
    }
}
