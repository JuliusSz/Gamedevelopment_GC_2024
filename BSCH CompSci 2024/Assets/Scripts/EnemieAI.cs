using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieAI : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int damage;
    public Vector2 playerLocation;
    public int speed;
    public int chaseSpeed;
    public int walkSpeed;
    public aIBehabiour state = aIBehabiour.Patrol;
    public float detectTime;
    public bool aggro;
    public float aggroTime;
    public float acceleration;
    public Animator animator;

    public int scoreVal;

    public GameManagerScript gameManager;

    private Coroutine aggroTimer;
    private Coroutine detectTimer;

    public Rigidbody2D myRb;

    public bool goRight;
    public enum aIBehabiour
    {
        Patrol,
        DetectPlayer,
        ChasePlayer,
    }
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
        case aIBehabiour.Patrol:
                animator.SetBool("patrol", true);
                animator.SetBool("detectPlayer", false);
                animator.SetBool("chase", false);
                speed = walkSpeed;
                if (Mathf.Abs(myRb.velocity.magnitude) < speed && goRight)
                {
                    myRb.AddForce(new Vector2(acceleration, 0), ForceMode2D.Force);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (Mathf.Abs(myRb.velocity.magnitude) < speed && !goRight)
                {
                    myRb.AddForce(new Vector2(acceleration * -1, 0), ForceMode2D.Force);
                    transform.localScale = new Vector3(1, 1, 1);
                }
                RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right));
                if (right && right.distance < 1 && right.collider.tag == "terrain")
                {
                    goRight = false;
                }

                RaycastHit2D left = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.left));
                if (left && left.distance < 1 && left.collider.tag == "terrain")
                {
                    goRight = true;
                }

                break;
        case aIBehabiour.DetectPlayer:
                animator.SetBool("patrol", false);
                animator.SetBool("detectPlayer", true);
                animator.SetBool("chase", false);
                speed = 0;
                break;
        case aIBehabiour.ChasePlayer:
                animator.SetBool("patrol", false);
                animator.SetBool("detectPlayer", false);
                animator.SetBool("chase", true);
                if (Mathf.Abs(myRb.velocity.magnitude) < speed && transform.position.x > playerLocation.x)
                {
                    myRb.AddForce(new Vector2(acceleration * -1, 0), ForceMode2D.Force);
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (Mathf.Abs(myRb.velocity.magnitude) < speed && transform.position.x < playerLocation.x)
                {
                    myRb.AddForce(new Vector2(acceleration , 0), ForceMode2D.Force);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                aggro = true;
                speed = chaseSpeed;
            break;
        }
        if(health <= 0)
        {
            gameManager.addScore(scoreVal);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            gameManager.damagePlayer(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && aggro == true)
        {
            playerLocation = collision.transform.position;
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
                state = aIBehabiour.Patrol;
            }
        }
    }
    IEnumerator AggroTime()
    {
        state = aIBehabiour.Patrol;
        aggro = false;
        yield return new WaitForSeconds(aggroTime);
    }
}
