using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    public float levelTime;
    public float timeLeft;
    public UnityEvent onServerDestroyed = new UnityEvent();
    public UnityEvent onSecondChange = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        timeLeft = levelTime;
        onServerDestroyed.AddListener(CheckServers);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            gameManager.GameOver("Out of time!");
        if ((int)timeLeft < (int)(timeLeft + Time.deltaTime))
            onSecondChange.Invoke();
    }

    public void DestroyServer(GameObject server)
    {
        Destroy(server);
        onServerDestroyed.Invoke();
    }
    void CheckServers()
    {
        if (ServerCount() == 1)
        {
            gameManager.Victory();
        }
    }

    public int ServerCount()
    {
        return GameObject.FindGameObjectsWithTag("Server").Length;
    }

    public void ClearProjectiles()
    {
        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
            Destroy(projectile);
    }
}
