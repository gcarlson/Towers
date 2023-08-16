using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portal;
    public float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = Time.time + 4.0f + Random.Range(-1.0f, 1.0f);
    }

    void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            var o = Instantiate(portal, transform.position, transform.rotation);
            GameManager.startingPos.Add(o.transform);
            nextSpawn += (4.0f + Random.Range(-1.0f, 1.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
