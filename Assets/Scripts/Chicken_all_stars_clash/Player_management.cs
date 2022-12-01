using System;
using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

public class Player_management : MonoBehaviour
{
    public Game_management GameManagement;
    public Enemy enemy;
    public List<Transform> playerSpawnerArena;
    public float startTimeRemain;
    public InputActionAsset inputAction;
    public PlayerInputManager inputManager;
    
    private int playerInputIndex;

    private void Start()
    {
        for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
            GameObject thisPlayer = GameManagement.playerClassChoosen[i];
            string controller = GameManagement.Controll[i];
            if(thisPlayer == null) continue;
            inputManager.playerPrefab = thisPlayer;
            inputManager.JoinPlayer(i,i,controller);
            GameManagement.playerClassChoosen[i].transform.position = playerSpawnerArena[i].transform.position;
        }
    }

    void Update()
    {
        if (startTimeRemain >= 0) {
            startTimeRemain -= Time.deltaTime;
        }
        else {
            enemy.EnemyStart();
        }
    }
}
