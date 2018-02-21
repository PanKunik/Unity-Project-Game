using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPath : MonoBehaviour {

    PlayerStats playerLvl;
	// Use this for initialization
	void Start () {
        playerLvl = GameObject.Find("Player").GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerLvl.GetLevel() >= 7)
            gameObject.SetActive(false);
	}
}
