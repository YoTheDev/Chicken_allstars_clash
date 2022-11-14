using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_select : MonoBehaviour
{

    [SerializeField] private GameObject crochet;
    [SerializeField] private List<GameObject> _class = new List<GameObject>();

    private float xAxis;
    private int uiIndex;

    private void Start()
    {
        if (gameObject.CompareTag("Player_01")) {
            _class.Add(GameObject.Find("P1/Class/Pirate")); _class.Add(GameObject.Find("P1/Class/Mage"));
            _class.Add(GameObject.Find("P1/Class/Science")); _class.Add(GameObject.Find("P1/Class/Thief"));
            crochet = GameObject.Find("P1/Validation");
            _class[1].SetActive(false); _class[2].SetActive(false); _class[3].SetActive(false);
            crochet.SetActive(false);
        }
    }

    void OnMove(InputValue Moving) {
        xAxis = Moving.Get<float>();
        if (xAxis <= -1) {
            _class[uiIndex].SetActive(false);
            if (uiIndex <= 0) uiIndex = 3;
            else uiIndex--;
            _class[uiIndex].SetActive(true);
        }
        if (xAxis >= 1) {
            _class[uiIndex].SetActive(false);
            if (uiIndex >= 3) uiIndex = 0;
            else uiIndex++;
            _class[uiIndex].SetActive(true);
        }
    }
}
