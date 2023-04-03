using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumPhysics : MonoBehaviour
{
    private Rigidbody2D body;
    private float previousVelocity;
    private float initialHeight;
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        initialHeight = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if ((previousVelocity > 0 && body.velocity.y < 0) && transform.position.y < initialHeight)
        {
            body.AddForce(body.velocity.normalized * 2 * body.mass, ForceMode2D.Impulse);
        }
        previousVelocity = body.velocity.y;
    }
}
