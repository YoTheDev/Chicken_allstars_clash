using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PatternSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player_management : MonoBehaviour
{
    public Game_management GameManagement;
    public Enemy enemy;
    public List<Transform> playerSpawnerArena;
    public float startTimeRemain;
    public PlayerInputManager inputManager;
    public List<GameObject> life;
    public GameObject ready;
    public GameObject go;
    public GameObject victoryUI;
    public GameObject defeatUI;
    public Button retryButton;
    public Button backButton;
    public Button defeatRetryButton;
    public Button defeatBackButton;
    public List<Player_class> playerClass;
    public TextMeshProUGUI score;
    public int timeBonus;
    
    [HideInInspector] public bool ActivateInput;
    [HideInInspector] public float scoreEarned;
    
    private float time;
    private int secondUpdate = 1;
    private int countPlayer;
    private bool playOneShot;

    private void Start() {
        GameManagement.victory = false;
        GameManagement.gameOver = false;
        retryButton.onClick.AddListener(() => Retry());
        backButton.onClick.AddListener(() => BackToTitle());
        defeatRetryButton.onClick.AddListener(() => Retry());
        defeatBackButton.onClick.AddListener(() => BackToTitle());
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        ready.SetActive(true);
        go.SetActive(false);
        for (int i = 0; i < life.Count; i++) {
            life[i].SetActive(false);
        }
        for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
            GameObject thisPlayer = GameManagement.playerClassChoosen[i];
            string controller = GameManagement.Controll[i];
            if(thisPlayer == null) continue;
            playerClass[i] = thisPlayer.GetComponent<Player_class>();
            inputManager.playerPrefab = thisPlayer;
            inputManager.JoinPlayer(i,i,controller);
            thisPlayer.transform.position = playerSpawnerArena[i].transform.position;
            life[i].SetActive(true);
        }
    }

    void Update() {
        if (GameManagement.victory && !playOneShot) {
            victoryUI.SetActive(true);
            for (int i = 0; i < GameManagement.playerClassChoosen.Count; i++) {
                if(GameManagement.playerClassChoosen[i] == null) continue;
                countPlayer++;
            }
            switch (countPlayer)
            {
                case 1:
                    scoreEarned += 10000;
                    break;
                case 2:
                    scoreEarned += 5000;
                    break;
                case 3:
                    scoreEarned += 2000;
                    break;
                case 4:
                    scoreEarned += 1000;
                    break;
            }
            scoreEarned += timeBonus;
            Invoke(nameof(ShowScore),1);
            backButton.Select();
            playOneShot = true;
        }
        if (startTimeRemain >= -1) {
            startTimeRemain -= Time.deltaTime;
        }
        else {
            time += Time.deltaTime;
            if (time >= secondUpdate && !GameManagement.victory) {
                secondUpdate++;
                timeBonus -= 100;
                playOneShot = false;
            }
            go.SetActive(false);
        }

        if (GameManagement.gameOver && !playOneShot) {
            defeatUI.SetActive(true);
            defeatRetryButton.Select();
            playOneShot = true;
        }
        if (!(startTimeRemain <= 0) || !(startTimeRemain > -1) || playOneShot) return;
        for (int i = 0; i < playerClass.Count; i++) {
            if(playerClass[i] == null) continue;
            ActivateInput = true;
        }
        go.SetActive(true);
        ready.SetActive(false);
        enemy.EnemyStart();
        playOneShot = true;
    }

    private void ShowScore() {
        score.text = scoreEarned.ToString();
    }
    public void Retry() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    public void BackToTitle() {
        SceneManager.LoadScene("Island_outside");
    }
}
