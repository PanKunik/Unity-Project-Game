using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public float timeToWalk = 0f;
    bool notCombat = false;

    public GameObject SpawnPoint;

    Transform target;
    NavMeshAgent nav;
    CharacterCombat combat;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        nav = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if( distance <= lookRadius )
        {
            notCombat = false;
            timeToWalk = 0f;

            nav.SetDestination(target.position);

            if( distance <= nav.stoppingDistance )
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                FaceTarget();
            }
        }
        else
        {
            if (notCombat)
            {
                timeToWalk = Random.Range(10f, 15f);
                notCombat = false;
            }

            timeToWalk -= Time.deltaTime;

            if( timeToWalk <= 0f  )
            {
                Vector3 destinationPoint = new Vector3(SpawnPoint.transform.position.x + Random.Range(-5,5), SpawnPoint.transform.position.y, SpawnPoint.transform.position.z + Random.Range(-5, 5));
                nav.SetDestination(destinationPoint);
                notCombat = true;
            }
        }
	}

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
