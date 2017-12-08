using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {
    // Use this for initialization

    Animator anim;
    public bool isArmed;

    // Use this for initialization
    void Start () {
        DiselectWeapons();
        
        anim = GameObject.Find("Player").GetComponent<Animator>();
        isArmed = false;
    }
	
	// Update is called once per frame
	void Update () {
        EquipWeapon();
	}

    public void SelectWeapon(int selectWeapon)
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectWeapon)
            {
                weapon.gameObject.SetActive(true);
                
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

    }

    public void DiselectWeapons()
    {
        foreach (Transform weapon in transform)
        {

            weapon.gameObject.SetActive(false);
        }
    }

    public void EquipWeapon()
    {

        if (isArmed)
        {

            anim.SetFloat("Weapon Index", 1);
        }
        else
        {
            anim.SetFloat("Weapon Index", 0);
        }
    }
}
