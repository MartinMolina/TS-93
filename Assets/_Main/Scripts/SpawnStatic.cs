using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStatic : MonoBehaviour
{
    public GameObject prefab;
    private GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
        GameObject.Find("Player").GetComponent<PlayerController>().onDeath.AddListener(SpawnObject);
    }

    // Update is called once per frame
    void SpawnObject()
    {
        if (item == null)
            item = Instantiate(prefab, transform.position, transform.rotation);
    }
}