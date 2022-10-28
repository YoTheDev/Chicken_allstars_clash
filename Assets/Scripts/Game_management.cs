using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Game manager",menuName = "Game_manager")]
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
    }
}
