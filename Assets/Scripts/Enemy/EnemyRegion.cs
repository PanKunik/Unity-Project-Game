using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegion : MonoBehaviour {

    GameObject player;
    GameObject [] enemy;
    EnemyMovement enemyMovement;
    bool playerInRegion = false;
    // Use this for initialization
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");


    }
    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRegion = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRegion = false;
        }
    }

    void SetEnemyAttack()
    {
        if (playerInRegion) {

            foreach (GameObject element in enemy)
            {
                element.GetComponent<EnemyMovement>().ResumeMove();
            }

        }
        else
        {
            foreach (GameObject element in enemy)
            {
                element.GetComponent<EnemyMovement>().StopMove();
            }

        }
       
       
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        SetEnemyAttack();
    }
}
