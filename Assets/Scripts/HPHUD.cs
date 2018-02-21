using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPHUD : MonoBehaviour {

    TextMeshProUGUI TMProArmor;
    PlayerStats Stats;
    // Use this for initialization
    void Start ()
    {
        TMProArmor = gameObject.GetComponent<TextMeshProUGUI>();
        Stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {
        TMProArmor.text = "HP: " + Stats.currentHealth + "/" + Stats.maxHealth;
    }
}
