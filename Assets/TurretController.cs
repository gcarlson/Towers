using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public enum Priority { FIRST, CLOSE, STRONG, RANDOM, WEAK };
    public Priority priority = Priority.FIRST;
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
    public int damageTotal = 0;
    public bool melee = false;

    // Start is called before the first frame update
    void Start()
    {
        nextShot = Time.time;
    }

    void FixedUpdate()
    {
        if (melee)
        {
            turret.transform.Rotate(0, 500 * Time.deltaTime, 0);
        }
    }

    private GameObject PickTarget(ArrayList targets)
    {
        GameObject best = null;
        float closest = 10000.0f;
        switch (priority)
        {
            case Priority.CLOSE:
                foreach (var enemy in targets)
                {
                    GameObject o = (GameObject)enemy;
                    float d = Vector3.Distance(transform.position, o.transform.position);
                    if (d < closest)
                    {
                        closest = d;
                        best = o;
                    }
                }
                return best;
            case Priority.STRONG:
                closest = -1.0f;
                foreach (var enemy in targets)
                {
                    GameObject o = (GameObject)enemy;
                    float d = o.GetComponent<EnemyHealth>().hp;
                    if (d > closest)
                    {
                        closest = d;
                        best = o;
                    }
                }
                return best;
            case Priority.WEAK:
                //closest = -1.0f;
                foreach (var enemy in targets)
                {
                    GameObject o = (GameObject)enemy;
                    float d = o.GetComponent<EnemyHealth>().hp;
                    if (d < closest)
                    {
                        closest = d;
                        best = o;
                    }
                }
                return best;
            case Priority.FIRST:
                foreach (var enemy in targets)
                {
                    GameObject o = (GameObject)enemy;
                    float d = o.GetComponent<HexMover>().distance;
                    if (d < closest)
                    {
                        closest = d;
                        best = o;
                    }
                }
                return best;
            case Priority.RANDOM:
            default:
               return (GameObject) targets[Random.Range(0, targets.Count)];
        }

        //return (GameObject) targets[Random.Range(0, targets.Count)];
    }
    public virtual bool canShoot()
    {
        return true;
    }

    public virtual void shoot(GameObject target, Quaternion rotation)
    {
        for (int i = 0; i < multishot; i++)
        {
            var o = Instantiate(bullet, transform.position, rotation);
            bulletSetup(o, target);
        }
    }
   public virtual void bulletSetup(GameObject o, GameObject target)
    {
        var v = o.transform.eulerAngles;
        v.y += Random.Range(0 - spread, spread);
        o.transform.eulerAngles = v;
        o.GetComponent<BulletController>().owner = this;
        o.GetComponent<Rigidbody>().velocity = (speed + Random.Range(0 - speedRange, speedRange)) * o.transform.forward.normalized;
        Destroy(o, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShot && canShoot())
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            var inRangeEnemies = new ArrayList();
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= range && enemy.GetComponent<HexMover>().visible)
                {   
                    inRangeEnemies.Add(enemy);
                }
            }
            
            if (inRangeEnemies.Count > 0)
            {
                nextShot = Time.time + 1.0f / fireRate;
                GameObject targetEnemy = PickTarget(inRangeEnemies);
                var targetPos = targetEnemy.transform.position;
                var lookPos = targetPos - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                if (turret)
                {
                    turret.transform.rotation = rotation;
                }
                if (projectile)
                {
                    shoot(targetEnemy, rotation);
                } else
                {
                    var o = Instantiate(bullet, targetPos + new Vector3(Random.Range(0 - spread, spread), 0.0f, Random.Range(0 - spread, spread)), rotation);
                    o.GetComponent<NapalmController>().owner = this;
                    Destroy(o, lifespan);
                }
            }
        }
    }
}
