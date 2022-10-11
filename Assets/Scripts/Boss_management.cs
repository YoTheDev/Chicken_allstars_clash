using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_management : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;
    [SerializeField] private int bossSpeed;
    // Start is called before the first frame update
    void Start()
    {
        players.Add(GameObject.FindWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        int RNG_players = Random.Range(0, players.Count);
        transform.position = Vector3.MoveTowards(transform.position,players[RNG_players].transform.position,bossSpeed * Time.deltaTime);
    }
}
