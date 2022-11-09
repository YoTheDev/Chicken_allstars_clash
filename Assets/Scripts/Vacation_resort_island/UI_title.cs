using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class UI_title : MonoBehaviour {
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private Button startButton;

    public List<GameObject> ui = new List<GameObject>();
    public List<GameObject> playerui = new List<GameObject>();
    public List<GameObject> player = new List<GameObject>();
    public List<GameObject> playerSpawner = new List<GameObject>();

    private bool _attackPressed;
    private bool _jumpPressed;
    private int _playeruiIndex;
    private int _playerIndex;
    private PlayerInputManager _inputManager;
    private PlayerInput _playerInput;

    void OnAttackHold() { _attackPressed = !_attackPressed; }
    void OnJumpHold() { _jumpPressed = !_jumpPressed; }
    
    private void Start() {
        titleScreen.SetActive(true);
        startButton.onClick.AddListener(() => { PlayerJoining(); } );
        _inputManager = GetComponent<PlayerInputManager>();
        _playerInput = GetComponent<PlayerInput>();
        for (int i = 0; i < ui.Count; i++) {
            ui[i].SetActive(false);
        }
        for (int i = 0; i < playerui.Count; i++) {
            playerui[i].SetActive(false);
        }
    }

    private void Update() {
        if (titleScreen.activeSelf && _jumpPressed && _attackPressed) {
            titleScreen.SetActive(false);
            ui[0].SetActive(true);
            _playerInput.enabled = false;
        }
    }
    
    public void PlayerJoining() {
        _inputManager.EnableJoining();
        _inputManager.JoinPlayer();
        playerui[_playeruiIndex].SetActive(true);
        _playeruiIndex++;
    }

    void OnPlayerJoined()
    {
        if (GameObject.FindWithTag("Player")) { player.Add(GameObject.FindWithTag("Player")); }
        player[_playerIndex].transform.position = playerSpawner[_playerIndex].transform.position;
    }
}
