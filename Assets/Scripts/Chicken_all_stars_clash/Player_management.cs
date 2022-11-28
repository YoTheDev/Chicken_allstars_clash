using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_management : MonoBehaviour
{
    public Game_management GameManagement;
    public Enemy enemy;
    public List<Transform> playerSpawnerArena;
    public float startTimeRemain;

    private int playerInputIndex;
    void Start() {
        for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
            GameObject thisPlayer = GameManagement.playerClassChoosen[i];
            if (thisPlayer == null) continue;
            Instantiate(thisPlayer);
            thisPlayer.transform.position = playerSpawnerArena[i].position;
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
