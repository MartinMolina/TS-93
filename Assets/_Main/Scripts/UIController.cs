using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private LevelManager levelManager;
    private GameManager gameManager;
    private PlayerController player;
    [SerializeField] TextMeshProUGUI destroyedServersTextBox;
    [SerializeField] TextMeshProUGUI serversTextBox;
    [SerializeField] TextMeshProUGUI timeTextBox;
    [SerializeField] TextMeshProUGUI livesTextBox;
    private int destroyedServers;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        timeTextBox.text = levelManager.levelTime.ToString(); 
        levelManager.onSecondChange.AddListener(UpdateTime);

        UpdateLives();
        player.onDeath.AddListener(UpdateLives);
        player.onDeath.AddListener(ResetServers);

        serversTextBox.text = levelManager.ServerCount().ToString();
        destroyedServers = 0;
        levelManager.onServerDestroyed.AddListener(AddDestroyed);
    }

    void UpdateTime()
    {
        timeTextBox.text = ((int)levelManager.timeLeft+1).ToString();
    }
    void UpdateLives()
    {
        livesTextBox.text = gameManager.lives.ToString();
    }

    void AddDestroyed()
    {
        destroyedServers++;
        destroyedServersTextBox.text = (destroyedServers).ToString();
    }

    void ResetServers()
    {
        destroyedServers = 0;
        destroyedServersTextBox.text = "0";
    }
}
