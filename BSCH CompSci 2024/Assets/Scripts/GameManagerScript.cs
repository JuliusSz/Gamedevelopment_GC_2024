using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Transform playerSpawnpoint;
    public float playerHealth;
    public float playerMaxHealth;
    public float playerScore;
    public GameObject player;
    public bool canBeDamaged;
    public float invincibalityTime;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        canBeDamaged = true;
        playerSpawnpoint = GameObject.FindWithTag("Start").transform;   
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void addScore(float scoreval)
    {
        playerScore += scoreval;
    }
    public void updateSpawn(Transform nextSpawnPoint) 
    {
        playerSpawnpoint = nextSpawnPoint;
    }
    public void respawnPlayer()
    {
        playerHealth = playerMaxHealth;
        player.transform.position =playerSpawnpoint.position;

    }
    public void damagePlayer(float damageDealt)
    {
        if(canBeDamaged) {
            canBeDamaged = false;
            playerHealth -= damageDealt;
            StartCoroutine(damageCooldown());
            if (playerHealth <= 0) 
            {
                respawnPlayer();
            }
        }
    }
    IEnumerator damageCooldown()
    {
        yield return new WaitForSeconds(invincibalityTime);
        canBeDamaged = true;
        yield return null;
    }
}
