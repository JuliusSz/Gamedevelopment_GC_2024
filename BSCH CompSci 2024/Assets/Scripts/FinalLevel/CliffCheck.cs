using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffCheck : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.parent.GetComponent<EnemieAI>().goRight == true)
        {
            transform.parent.GetComponent<EnemieAI>().goRight = true;
        }
        else
        {
            transform.parent.GetComponent<EnemieAI>().goRight = false;
        }
    }
}
