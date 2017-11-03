using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRotation;
    Animator anim;

    float rotationSpeed = 15f;
    float speed = 3f;
    float camRayLength = 1000f;
    int floorMask;
    bool moving = false;

    // Use this for initialization
    void Awake ()
    { 
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }

        if (moving)
        {
            Move();
            anim.SetBool("IsWalking", moving);
        }

        if( Input.GetMouseButton(1))
        {
            if (anim.GetBool("IsSwordEquipped"))
                anim.SetBool("IsSwordEquipped", false);
            else
                anim.SetBool("IsSwordEquipped", true);
        }
    }

    void SetTargetPosition()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if( Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            targetPosition = floorHit.point;
            // this.transform.LookAt(targetPosition);
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z);
            playerRotation = Quaternion.LookRotation(lookAtTarget);
            moving = true;
        }
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
            moving = false;
    }
}
