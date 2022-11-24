using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_select : MonoBehaviour
{
    [SerializeField] private GameObject crochet;
    [SerializeField] private Button readyButton;
    [SerializeField] private List<GameObject> _class = new List<GameObject>();
    [SerializeField] private List<GameObject> _classObject = new List<GameObject>();
    [SerializeField] private Game_management playerData;
    [SerializeField] private string playerTag;
    [SerializeField] private string playerUI;

    private float xAxis;
    private int uiIndex;
    private bool validate;
    private UI_title ui;

    private void Start() {
        readyButton = GameObject.Find("Ready").GetComponent<Button>();
        ui = FindObjectOfType<UI_title>();
        ui.playerConnected++;
        if (gameObject.CompareTag(playerTag)) {
            _class.Add(GameObject.Find(playerUI+"/Class/Pirate")); 
            _class.Add(GameObject.Find(playerUI+"/Class/Mage"));
            _class.Add(GameObject.Find(playerUI+"/Class/Science")); 
            _class.Add(GameObject.Find(playerUI+"/Class/Thief"));
            crochet = GameObject.Find(playerUI+"/Validation");
            ButtonAdd();
        }
    }

    void ButtonAdd() {
        for (int i = 1; i < _class.Count; i++) _class[i].SetActive(false);
        crochet.SetActive(false);
    }

    void OnMove(InputValue Moving) {
        xAxis = Moving.Get<float>();
        if (validate) return;
        if (xAxis <= -1) {
            _class[uiIndex].SetActive(false);
            _classObject[uiIndex].SetActive(false);
            if (uiIndex <= 0) uiIndex = 3;
            else uiIndex--;
            _class[uiIndex].SetActive(true);
            _classObject[uiIndex].SetActive(true);
        }
        if (xAxis >= 1) {
            _class[uiIndex].SetActive(false);
            _classObject[uiIndex].SetActive(false);
            if (uiIndex >= 3) uiIndex = 0;
            else uiIndex++;
            _class[uiIndex].SetActive(true);
            _classObject[uiIndex].SetActive(true);
        }
    }

    void OnJump() {
        if (validate) return;
        crochet.SetActive(true);
        validate = true;
        ui.playerReadyCount++;
    }

    void OnBack() {
        if (ui.ui[2].activeSelf) return;
        if (!validate) return;
        crochet.SetActive(false);
        readyButton.interactable = false;
        ui.playerReadyCount--;
        validate = false;
    }
}
