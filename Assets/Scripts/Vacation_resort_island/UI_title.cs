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
    [SerializeField] private GameObject grayOpacity;
    [SerializeField] private Button startButton;
    [SerializeField] private Button readyButton;

    public List<GameObject> ui = new List<GameObject>();
    public List<GameObject> playerui = new List<GameObject>();
    public List<GameObject> player = new List<GameObject>();
    public List<GameObject> playerSpawner = new List<GameObject>();

    private bool _attackPressed;
    private bool _jumpPressed;
    private bool _playOneShot;
    private int _playerIndex = 0;
    private PlayerInputManager _inputManager;
    private PlayerInput _playerInput;

    void OnAttackHold() { _attackPressed = !_attackPressed; }
    void OnJumpHold() { _jumpPressed = !_jumpPressed; }
    
    private void Start() {
        _playOneShot = true;
        titleScreen.SetActive(true);
        startButton.onClick.AddListener(() => { PlayerJoining(); } );
        readyButton.onClick.AddListener(() => { MorePlayer(); } );
        readyButton.interactable = false;
        _inputManager = GetComponent<PlayerInputManager>();
        _playerInput = GetComponent<PlayerInput>();
        for (int i = 0; i < ui.Count; i++) { ui[i].SetActive(false); }
        for (int i = 0; i < playerui.Count; i++) { playerui[i].SetActive(false); }
    }

    private void Update() {
        if (titleScreen.activeSelf && _jumpPressed && _attackPressed) {
            titleScreen.SetActive(false);
            ui[0].SetActive(true);
            _playerInput.enabled = false;
        }
    }
    
    public void PlayerJoining() {
        if (_playOneShot) {
            _inputManager.EnableJoining();
            _playOneShot = false;
        }
    }

    public void MorePlayer() {
        if (_playerIndex != player.Count) {
            _inputManager.DisableJoining();
            ui[2].SetActive(true);
            grayOpacity.SetActive(true);
        }
        else {
            Debug.Log("beggin");
        }
    }

    void JoinedPlayer() {
        if(_playerIndex >= playerSpawner.Count || _playerIndex >= player.Count) return;
        player[_playerIndex].transform.position = playerSpawner[_playerIndex].transform.position;
        _inputManager.playerPrefab = player[_playerIndex+1];
        playerui[_playerIndex].SetActive(true);
        _playerIndex++;
    }

    void OnPlayerJoined() {
        if (ui[1].activeSelf != true) return;
        JoinedPlayer();
    }
}
