using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new playerData", menuName = "ChickenAllStarsClash/playerData")]
public class playerData : ScriptableObject
{
    public int playerOrder;
    public List<GameObject> playerClass = new List<GameObject>();
}
