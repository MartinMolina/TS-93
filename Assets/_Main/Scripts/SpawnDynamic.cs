using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDynamic : MonoBehaviour
{
    public GameObject prefab;
    private GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = Instantiate(prefab, transform.position, transform.rotation);
        GameObject.Find("Player").GetComponent<PlayerController>().onDeath.AddListener(ResetObject);
    }

    // Update is called once per frame
    void ResetObject()
    {
        Destroy(instance);
        instance = Instantiate(prefab, transform.position, transform.rotation);
    }
}
