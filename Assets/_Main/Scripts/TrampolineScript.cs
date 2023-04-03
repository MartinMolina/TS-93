using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    public float trampolinePower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D body = collision.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(body.velocity.x, trampolinePower);
    }
}
