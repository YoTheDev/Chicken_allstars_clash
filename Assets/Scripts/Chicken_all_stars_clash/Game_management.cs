using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PatternSystem;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new Game manager",menuName = "ChickenAllStarsClash/InGame/Game_manager")]
public class Game_management : ScriptableObject
{
    [SerializeField] private List<bool> playerAlive;
    [SerializeField] private List<GameObject> playerClass;
    
    public List<GameObject> playerClassChoosen;
    public List<int> ControllerOrder;
    public int _aliveIndex;
    public int _classIndex;
    public List<string> Controll;
    public bool victory;
    public bool gameOver;

    public void PlayerCount() {
        playerAlive[_aliveIndex] = true;
        playerClassChoosen[_aliveIndex] = playerClass[_classIndex];
    }
    
    public void PlayerLeft() {
        playerAlive[_aliveIndex] = false;
        playerClassChoosen[_aliveIndex] = null;
    }
    
    public void PlayerDead() {
        playerAlive[_aliveIndex] = false;
        foreach (bool currentPlayer in playerAlive) {
            if (currentPlayer) return;
        }
        GameOver();
    }

    public void GameOver() { gameOver = true; }
    public void Victory() { victory = true; }

    private void OnEnable() {
        for (int i = 0; i < playerClassChoosen.Count; i++) {
            playerClassChoosen[i] = null;
        }
        for (int i = 0; i < playerAlive.Count; i++) {
            playerAlive[i] = false;
        }
        for (int i = 0; i < ControllerOrder.Count; i++) {
            ControllerOrder[i] = "\0"[0];
        }
        for (int i = 0; i < Controll.Count; i++) {
            Controll[i] = null;
        }
    }
}
