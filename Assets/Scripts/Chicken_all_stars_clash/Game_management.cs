using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PatternSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "new Game manager",menuName = "ChickenAllStarsClash/InGame/Game_manager")]
public class Game_management : ScriptableObject
{
    [SerializeField] private List<bool> playerAlive;
    [SerializeField] private List<GameObject> playerClass;
    [SerializeField] private List<GameObject> playerClassChoosen;

    public int _aliveIndex;
    public int _classIndex;

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

    public bool GameOver() {
        Debug.Log("GameOver");
        return true;
    }

    public bool Victory() {
        Debug.Log("Victory!");
        return true;
    }

    private void OnEnable() {
        for (int i = 0; i < playerClassChoosen.Count; i++) {
            playerClassChoosen[i] = null;
        }
        for (int i = 0; i < playerAlive.Count; i++) {
            playerAlive[i] = false;
        }
    }
}
