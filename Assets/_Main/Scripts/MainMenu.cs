using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame

    private void StartGame()
    {
        SceneManager.LoadScene("Level "+GameObject.Find("Game Manager").GetComponent<GameManager>().level);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
