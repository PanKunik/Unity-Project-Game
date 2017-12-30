using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public float timeToWalk = 0f;
    bool notCombat = false;
    public bool isActive = true;

    public GameObject SpawnPoint;

    Transform target;
    NavMeshAgent nav;
    CharacterCombat combat;
    Animator anim;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        nav = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if( distance <= lookRadius && isActive)
        {
            nav.stoppingDistance = 2f;
            notCombat = false;
            timeToWalk = 0f;
            
            nav.SetDestination(target.position);
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsCombat", true);

            if ( distance <= nav.stoppingDistance )
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                    anim.SetBool("IsWalking", false);
                }

                FaceTarget();
            }
        }
        else
        {
            nav.stoppingDistance = 0f;
            anim.SetBool("IsCombat", false);

            if (notCombat)
            {
                timeToWalk = Random.Range(10f, 15f);
                notCombat = false;
            }

            if(nav.remainingDistance <= 0.1f)
                anim.SetBool("IsWalking", false);

            timeToWalk -= Time.deltaTime;

            if( timeToWalk <= 0f  )
            {
                Vector3 destinationPoint = new Vector3(SpawnPoint.transform.position.x + Random.Range(-5,5), 0, SpawnPoint.transform.position.z + Random.Range(-5, 5));
                nav.SetDestination(destinationPoint);
                anim.SetBool("IsWalking", true);
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
