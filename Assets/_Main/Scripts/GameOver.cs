using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private GameManager gameManager;
    public Button menuButton;
    public TextMeshProUGUI reason;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        reason.text = gameManager.reason;
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    private void ReturnToMenu()
    {
        Destroy(gameManager.gameObject);
        SceneManager.LoadScene("Main Menu");
    }
}