using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Shotgun shg;
    public TextMeshProUGUI tmpAmmo;
    public GameManagerScript gameManager;
    public TextMeshProUGUI score;
    public TextMeshProUGUI health;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "HP:" + gameManager.playerHealth + "/" + gameManager.playerMaxHealth;
        score.text = "Score: " + gameManager.playerScore.ToString();
        tmpAmmo.text = shg.crntAmmo.ToString() + "/" + shg.maxAmmo.ToString();
    }
}
