using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                Debug.Log("enemy hit");
                collision.transform.GetComponent<EnemieAI>().health --;
            }
            Destroy(this.gameObject);
        }

    }
}
