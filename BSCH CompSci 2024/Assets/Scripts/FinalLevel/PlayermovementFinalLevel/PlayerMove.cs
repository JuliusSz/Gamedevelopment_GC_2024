using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public Rigidbody2D myRb;
    public bool isGrounded;
    public float Jumpforce;
    public Animator animator;
    public Shotgun sg;
    public Vector2 mousePos;

    public bool walkLeft;

    // Start is called before the first frame update
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(myRb.velocity.x));
        mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        if (mousePos.x < Screen.width / 2)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (mousePos.x > Screen.width / 2)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }

        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && Mathf.Abs(Input.GetAxis("Horizontal")) >= 0)
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"), 0), ForceMode2D.Force);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            myRb.AddForce(new Vector2(0, Jumpforce), ForceMode2D.Impulse);
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
}
