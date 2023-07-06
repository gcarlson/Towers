using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public int multishot = 1;
    public float speed = 30.0f;
    public float lifespan = 3.0f;
    public float fireRate = 10.0f;
    public float speedRange = 0.0f;
    public float spread = 0.0f;
    public float range = 20.0f;
    public bool projectile = true;
    public GameObject turret;
    public GameObject bullet;
    private float nextShot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        nextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShot)
        {
            nextShot += 1.0f / fireRate;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            var inRangeEnemies = new ArrayList();
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
                {
                    inRangeEnemies.Add(enemy);
                }
            }
            
            if (inRangeEnemies.Count > 0)
            {
                var targetPos = ((GameObject)inRangeEnemies[Random.Range(0, inRangeEnemies.Count)]).transform.position;
                var lookPos = targetPos - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                turret.transform.rotation = rotation;

                if (projectile)
                {
                    for (int i = 0; i < multishot; i++)
                    {
                        var o = Instantiate(bullet, transform.position, rotation);
                        var v = o.transform.eulerAngles;
                        v.y += Random.Range(0 - spread, spread);
                        o.transform.eulerAngles = v;
                        o.GetComponent<Rigidbody>().velocity = (speed + Random.Range(0 - speedRange, speedRange)) * o.transform.forward.normalized;

                        //o.GetComponent<Rigidbody>().velocity = speed * lookPos.normalized;
                        Destroy(o, lifespan);
                    }
                } else
                {
                    var o = Instantiate(bullet, targetPos + new Vector3(Random.Range(0 - spread, spread), 0.0f, Random.Range(0 - spread, spread)), rotation);
                    Destroy(o, lifespan);
                }
            }
        }
    }
}
