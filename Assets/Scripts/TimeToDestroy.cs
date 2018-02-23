using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDestroy : MonoBehaviour {

	// Use this for initialization
	public void DestroyMe () {
        Destroy(gameObject);
	}
}
