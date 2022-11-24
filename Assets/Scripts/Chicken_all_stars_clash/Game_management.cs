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
    [SerializeField] int aliveIndex;
    
    public void PlayerCount() {
        if (playerAlive.Count > 4) return;
        playerAlive.Add(true);
    }

    public void PlayerDead() {
        playerAlive[aliveIndex] = false;
        if (aliveIndex < playerAlive.Count) aliveIndex++;
        else GameOver();
    }

    public bool GameOver() {
        if (playerAlive.Last() == false && Victory() == false) {
            Debug.Log("GameOver");
            playerAlive.Clear();
            return true;
        }
        return false;
    }

    public bool Victory() {
        Debug.Log("Victory!");
        playerAlive.Clear();
        return true;
    }
}
