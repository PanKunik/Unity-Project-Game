using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    const float movementAnimationSmoothTime = .1f;

    Animator anim;
    NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float speedPercent = nav.velocity.magnitude / nav.speed;
        anim.SetFloat("speedPercent", speedPercent, movementAnimationSmoothTime, Time.deltaTime);
	}
}
