using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int lives;
    public string reason;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SubstractLife()
    {
        if (lives == 0)
            GameOver("Out of lives!");
        else
            lives--;
    }

    public void Victory()
    {
        level++;
        lives++;
        SceneManager.LoadScene("Victory");
    }

    public void GameOver(string text)
    {
        reason = text;
        SceneManager.LoadScene("Game Over");
    }
}
