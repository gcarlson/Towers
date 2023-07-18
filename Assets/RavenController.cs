using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenController : MonoBehaviour
{
    public GameObject target;
    public OdinController owner;
    private Vector3 targetPos;
    private bool enRoute = true;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FindNearest()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var inRangeEnemies = new ArrayList();
        float d = Mathf.Infinity;
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(targetPos, enemy.transform.position) < d)
            {
                target = enemy;
                d = Vector3.Distance(targetPos, enemy.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        if (enRoute && !target)
        {
            FindNearest();
            if (!target)
            {
                enRoute = false;
        }
        }
            targetPos = enRoute ? target.transform.position : owner.transform.position;

        if (enRoute) { targetPos.y = 5; }
            if ((transform.position - targetPos).magnitude < 0.5f)
            {
            if (enRoute)
            {
                targetPos.y = 0.5f;
                var o = Instantiate(projectile, targetPos, transform.rotation);
                o.GetComponent<NapalmController>().owner = owner;
                Destroy(o, 0.5f);
                enRoute = false;
            } else
            {
                owner.birdsReady++;
                Destroy(gameObject);
            }
            }
            else
            {
                transform.position += 25.0f * (targetPos - transform.position).normalized * Time.deltaTime;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
