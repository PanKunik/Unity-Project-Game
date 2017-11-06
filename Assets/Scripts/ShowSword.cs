using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSword : MonoBehaviour {

    //bool isActive = false;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ( Input.GetKey(KeyCode.E) )
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                //isActive = false;
            }
            else
            {
                gameObject.SetActive(true);
                //isActive = true;
            }
        }
	}
}
