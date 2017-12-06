using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipWeapon : MonoBehaviour {

    Animator anim;
    bool isArmed = false;
    public GameObject sword;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        sword.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isArmed)
            {
                anim.SetFloat("Weapon Index", 1);
                isArmed = true;
                sword.SetActive(true);
            }
            else
            {
                anim.SetFloat("Weapon Index", 0);
                isArmed = false;
                sword.SetActive(false);
            }
        
        }
	}
}
