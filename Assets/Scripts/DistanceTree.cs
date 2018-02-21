using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTree : MonoBehaviour {

    public float distance;
    public Terrain terrain;
    void Start()
    {
        terrain.treeDistance = distance;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
