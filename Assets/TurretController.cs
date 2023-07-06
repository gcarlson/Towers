using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject turret;
    public GameObject bullet;
    private float nextShot = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShot)
        {
            nextShot += 0.1f;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                var lookPos = enemies[Random.Range(0, enemies.Length)].transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                turret.transform.rotation = rotation;

                var o = Instantiate(bullet, transform.position, rotation);
                o.GetComponent<Rigidbody>().velocity = 30.0f * lookPos.normalized;
                Destroy(o, 3.0f);
            }
        }
    }
}
