using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public TMP_Text scoreText;
    public TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameManager.playerScore;
        healthText.text = "Health: " + gameManager.playerHealth;
    }
}
