using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipWeapon : MonoBehaviour {
    public enum Weapon { tomahawk,sword}
    Animator anim;
    public bool isArmed = false;
    GameObject weapon;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        weapon = GameObject.Find("Sword");
        GameObject.Find("tomahawk").SetActive(false);
        weapon.SetActive(false);
        isArmed = false;
    }

    // Update is called once per frame
    void Update() {
        EquipWeapon();


    }

    public void EquipWeapon()
    {
        
        if (isArmed)
        {
            
            anim.SetFloat("Weapon Index", 1);
            weapon.SetActive(true);
        }
        else
        {
            anim.SetFloat("Weapon Index", 0);
            weapon.SetActive(false);
        }
    }
    public void ChangeWeapon(Weapon index)
    {
        
        switch((int)index)
        {
            case 0:
                weapon.SetActive(true);
                weapon = GameObject.Find("tomahawk");
                
                break;
            case 1:
                weapon = GameObject.Find("Sword");
                break;
        }
    }
}
