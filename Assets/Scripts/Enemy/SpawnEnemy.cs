using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject Enemy;

    public bool dead = false;

    public float spawnTimer = 15f;

    IEnumerator SpawnDelay(float timer)
    {
        while (timer >= 0)
        {
            timer -= 1f;
            yield return new WaitForSeconds(1f);
        }

        Enemy.GetComponent<EnemyStats>().Spawn();
        yield return null;
    }
}
