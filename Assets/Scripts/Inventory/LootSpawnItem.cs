using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootSpawnItem {

    public GameObject spawnItem;
    public int minCount;
    public int maxCount;
    public float chance;
}
