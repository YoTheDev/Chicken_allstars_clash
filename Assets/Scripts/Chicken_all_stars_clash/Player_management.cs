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
    public List<int> ControllOrder;
    public List<string> Controll;
    public string currentControllScheme;
    public int currentControlSchemeOrder;

    private int playerInputIndex;
    private PlayerInputManager playerInputManager;
    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
            GameObject thisPlayer = GameManagement.playerClassChoosen[i];
            if (thisPlayer == null) continue;
            playerInputManager.JoinPlayer(i,i,Controll[i]);
            playerInputManager.playerPrefab = GameManagement.playerClassChoosen[i];
            Debug.Log(playerInputManager.playerPrefab);
            //currentControllScheme = thisPlayer.GetComponent<PlayerInput>().defaultControlScheme;
            //Controll[i] = GameManagement.Controll[i];
            //currentControllScheme = Controll[i];
            //currentControlSchemeOrder = currentControllScheme[0];
            //ControllOrder[i] = GameManagement.ControllerOrder[i];
            //currentControlSchemeOrder = ControllOrder[i];
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
