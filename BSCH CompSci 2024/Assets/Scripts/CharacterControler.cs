using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public Rigidbody2D myRb;
    public bool isGrounded;
    public float Jumpforce;
    public float secondaryJumpforce;
    public float secondaryJumpDelay;
    public bool secondaryJumpbool;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed",Mathf.Abs(myRb.velocity.x));
        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && Mathf.Abs(Input.GetAxis("Horizontal")) >= 0)
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"),0), ForceMode2D.Force);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
           
            myRb.AddForce(new Vector2(0, Jumpforce), ForceMode2D.Impulse);
            StartCoroutine(secondaryJump());
        }

        if (Input.GetButton("Jump")&& secondaryJumpbool && isGrounded == false)
        {
            myRb.AddForce(new Vector2(0, secondaryJumpforce), ForceMode2D.Force); 
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }
    IEnumerator secondaryJump()
    {
        secondaryJumpbool = true;
        yield return new WaitForSeconds(secondaryJumpDelay);
        secondaryJumpbool = false;
        yield return null;
    }
}
