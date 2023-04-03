using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float timeToExplode;
    private float timeLeft;
    private bool timerOn;

    public float explosionRadius;
    public float explosionPower;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToExplode;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box"))
                {
                    Push(box);
                }
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<SawController>())
                        Push(enemy);
                }
                foreach (GameObject server in GameObject.FindGameObjectsWithTag("Server"))
                {
                    Vector3 difference = server.transform.position - transform.position;
                    float distance = difference.magnitude;
                    if (distance < explosionRadius)
                    {
                        GameObject.Find("Level Manager").GetComponent<LevelManager>().DestroyServer(server);
                    }
                }
                Push(GameObject.Find("Player"));
                Destroy(gameObject);
            }
        }
    }

    private void Push(GameObject objective)
    {
        Vector3 difference = objective.transform.position - transform.position;
        float distance = difference.magnitude;
        if (distance < explosionRadius)
        {
            Vector3 forceDirection = difference.normalized;
            float intensity = (1 - distance / explosionRadius) * explosionPower;
            objective.GetComponent<Rigidbody2D>()?.AddForce(forceDirection * intensity, ForceMode2D.Impulse);
            objective.GetComponent<PlayerController>()?.Disable(objective.GetComponent<PlayerController>().stunDuration);
            if (objective.GetComponent<SawController>())
            {
                objective.GetComponent<SawController>().enabled = false;
                objective.GetComponent<Rigidbody2D>().freezeRotation = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            timerOn = true;
    }
}
