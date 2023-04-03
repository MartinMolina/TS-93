using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Button NextButton;

    [SerializeField] private Button MenuButton;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager.level == 4)
        {
            Destroy(NextButton.gameObject);
        }
        else
            NextButton.onClick.AddListener(NextLevel);
        MenuButton.onClick.AddListener(MainMenu);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(GameObject.Find("Game Manager").GetComponent<GameManager>().level == 2 ? "Level 2" : "Level 3");
    }

    private void MainMenu()
    {
        Destroy(gameManager.gameObject);
        SceneManager.LoadScene("Main Menu");
    }
}
