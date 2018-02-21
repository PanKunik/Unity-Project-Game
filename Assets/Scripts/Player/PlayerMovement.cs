using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

    Transform target;
    NavMeshAgent nav;
    public NavMeshAgent GetNavMeshAgent()
    {
        return nav;
    }
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if( target != null )
        {
            nav.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        nav.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        nav.stoppingDistance = newTarget.radius * .8f;
        nav.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        nav.stoppingDistance = 0f;
        nav.updateRotation = true;

        target = null;
    }

    public void StopFollowingTargetOnDeath()
    {
        nav.updateRotation = true;

        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        nav.speed += movementSpeed;
    }
}
