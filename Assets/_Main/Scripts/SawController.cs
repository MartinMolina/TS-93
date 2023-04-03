using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;

    public Transform groundDetectionPoint;
    public LayerMask groundDetectionMask;
    public float groundDetectionHeight;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionHeight, groundDetectionMask);
        if (!hit)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = transform.right * speed;
    }
}
