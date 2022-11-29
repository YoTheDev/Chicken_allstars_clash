using System.Collections;
using System.Collections.Generic;
using PatternSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player_management : MonoBehaviour
{
    public Game_management GameManagement;
    public Enemy enemy;
    public List<Transform> playerSpawnerArena;
    public float startTimeRemain;
    public char ControllOrder;
    public string Controll;
    public string currentControlScheme;

    private int playerInputIndex;
    void Start() {
        for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
            GameObject thisPlayer = GameManagement.playerClassChoosen[i];
            if (thisPlayer == null) continue;
            Instantiate(thisPlayer);
            currentControlScheme = thisPlayer.GetComponent<PlayerInput>().currentControlScheme;
            Controll = GameManagement.Controll[i];
            currentControlScheme = Controll;
            ControllOrder = GameManagement.ControllerOrder[i];
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
