using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemieAI : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int damage;
    public Transform playerLocation;
    public int speed;
    public int chaseSpeed;
    public int walkSpeed;
    public aIBehabiour state = aIBehabiour.Idle;
    public float detectTime;
    public bool aggro;
    public float aggroTime;
    private Coroutine aggroTimer;
    private Coroutine detectTimer;
    public enum aIBehabiour
    {
        Idle,
        Patrol,
        DetectPlayer,
        ChasePlayer,
        AggroIdle
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
        case aIBehabiour.Idle:
                speed = 0;
                aggro = false;
                break;
        case aIBehabiour.Patrol:
                speed = walkSpeed;
                break;
        case aIBehabiour.DetectPlayer:
                speed = 0;
                break;
        case aIBehabiour.ChasePlayer:
                aggro = true;
                speed = chaseSpeed;
            break;
        case aIBehabiour.AggroIdle:
                
                aggro = true;
                speed = 0;
            break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && aggro == true)
        {
            StopCoroutine("AggroTime");
            state = aIBehabiour.ChasePlayer;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && aggro == false)
        {
            StartCoroutine("DetectTime");
            StopCoroutine("AggroTime");

        }
        else if (collision.gameObject.CompareTag("Player") && aggro == true )
        {
            StopCoroutine("AggroTime");
            state = aIBehabiour.ChasePlayer;
        }
    }
    IEnumerator DetectTime()
    {   
        state = aIBehabiour.DetectPlayer;
        yield return new WaitForSeconds(detectTime);
        aggro = true;
        state = aIBehabiour.ChasePlayer;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine("DetectTime");
            StopCoroutine("AggroTime");
            if(state != aIBehabiour.DetectPlayer)
            {
                StartCoroutine("AggroTime");
            }
            if (!aggro)
            {
                state = aIBehabiour.Idle;
            }
        }
    }
    IEnumerator AggroTime()
    {
        state = aIBehabiour.AggroIdle;
        yield return new WaitForSeconds(aggroTime);
        state = aIBehabiour.Idle;
    }
}
