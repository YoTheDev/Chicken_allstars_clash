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
    [SerializeField] int aliveIndex;
    
    public void PlayerCount() {
        playerAlive.Add(true);
    }

    public void PlayerDead() {
        playerAlive[aliveIndex] = false;
        if (aliveIndex < playerAlive.Count) aliveIndex++;
        else GameOver();
    }

    public bool GameOver() {
        if (playerAlive.Last() == false) {
            Debug.Log("GameOver");
            return true;
        }
        return false;
    }

    public bool Victory() {
        Debug.Log("Victory!");
        return true;
    }
}
