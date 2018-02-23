using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageHUD : MonoBehaviour
{

    TextMeshProUGUI TMProArmor;
    PlayerStats Stats;

    // Use this for initialization
    void Start()
    {
        TMProArmor = gameObject.GetComponent<TextMeshProUGUI>();
        Stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        TMProArmor.text = "DAMAGE: " + Stats.minDamage.GetValue() + " - " + Stats.maxDamage.GetValue();
    }
}
