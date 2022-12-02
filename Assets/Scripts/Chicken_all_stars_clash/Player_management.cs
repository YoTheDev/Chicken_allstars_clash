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
    public List<GameObject> life;
    public GameObject ready;
    public GameObject go;
    
    private int playerInputIndex;

    private void Start() {
        ready.SetActive(true);
        go.SetActive(false);
        for (int i = 0; i < life.Count; i++) {
            life[i].SetActive(false);
        }
        for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
            GameObject thisPlayer = GameManagement.playerClassChoosen[i];
            string controller = GameManagement.Controll[i];
            if(thisPlayer == null) continue;
            inputManager.playerPrefab = thisPlayer;
            inputManager.JoinPlayer(i,i,controller);
            GameManagement.playerClassChoosen[i].transform.position = playerSpawnerArena[i].transform.position;
            life[i].SetActive(true);
        }
    }

    void Update()
    {
        if (startTimeRemain >= -1) {
            startTimeRemain -= Time.deltaTime;
        }
        else {
            enemy.EnemyStart();
            go.SetActive(false);
        }

        if (startTimeRemain <= 0 && startTimeRemain > -1) {
            go.SetActive(true);
            ready.SetActive(false);
        }
    }
}
