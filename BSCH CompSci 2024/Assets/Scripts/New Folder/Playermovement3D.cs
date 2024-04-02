using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Playermovement3D : MonoBehaviour
{
    public Animator animator;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        animator.SetFloat("velocity", speed);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hit.point;
            }
        }
    }
}
