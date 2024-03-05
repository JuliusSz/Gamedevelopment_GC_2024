using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    // Start is called before the first frame update

    public float scoreValue;
    public GameManagerScript gameManager;
    public GameObject pickupEffect;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {
            gameManager.addScore(scoreValue);
            Instantiate(pickupEffect, transform.position,transform.rotation);
            Destroy(gameObject);
        }
        


    }
}
