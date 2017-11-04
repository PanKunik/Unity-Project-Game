using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    Animator anim;
    GameObject player;
    bool isOpen = false;
    // Use this for initialization
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            anim.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            anim.SetBool("isOpen", false);
        }
    }
    private void FixedUpdate()
    {
        //anim.SetBool("isOpen", isOpen);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
