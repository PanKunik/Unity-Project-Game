using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGizmos : MonoBehaviour {

    public float spawnRadius = 10f;
    Vector3 SpawnPoint;

	// Use this for initialization
	void Start () {
        SpawnPoint = new Vector3(spawnRadius, 2, spawnRadius);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, SpawnPoint);
    }
}
