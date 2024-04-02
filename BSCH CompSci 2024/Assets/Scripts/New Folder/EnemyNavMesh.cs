using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public Transform player;
    public bool aggro;
    public bool chasing;
    public bool destinationReached;
    public float destinationReachDisatnce;
    public Transform[] patrolpoints;
    public float aggrotimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;      
        destinationReached = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        animator.SetFloat("velocity", speed);

        if(aggro == false && destinationReached == true)
        {
            destinationReached = false;
            GetComponent<NavMeshAgent>().destination = patrolpoints[UnityEngine.Random.Range(0, patrolpoints.Length)].position;
        }

        if(aggro == true)
        {
            GetComponent<NavMeshAgent>().destination = player.position;
        }

        if(Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination)< destinationReachDisatnce)
        {
            destinationReached = true;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            StopCoroutine("ChaseCooldown");
            aggro = true;
            chasing = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<NavMeshAgent>().destination = player.position;
            chasing = false;
            StopCoroutine("ChaseCooldown");
            StartCoroutine("ChaseCooldown");
        }
    }
    IEnumerator ChaseCooldown()
    {
        yield return new WaitForSeconds(aggrotimer);
        aggro =false;
        destinationReached = true;
    }
}
