using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
            Destroy(collision.gameObject);
    }
}
