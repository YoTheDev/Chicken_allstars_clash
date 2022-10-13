using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_management : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;
    [SerializeField] private int bossSpeed;

    private bool isFinished;
    private int phase;
    private int Attack;
    // Start is called before the first frame update
    void Start()
    {
        players.Add(GameObject.FindWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished) {
            switch (phase)
            {
                case 1:
                    switch (Attack)
                    {
                        
                    }
                    break;
            }
        }
    }
}
