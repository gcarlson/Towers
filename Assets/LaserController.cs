using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : TurretController
{
    public GameObject lightning;
    public override void shoot(GameObject target, Quaternion rotation)
    {
        var o = Instantiate(lightning);
        var l = o.GetComponent<LineRenderer>();
        
        List<GameObject> targets = new List<GameObject>();
        targets.Add(target);
        for (int i = 0; i < 3; i++)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            List<GameObject> inRangeEnemies = new List<GameObject>();
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(target.transform.position, enemy.transform.position) <= 8.0f && enemy.GetComponent<HexMover>().visible && !targets.Contains(enemy))
                {
                    inRangeEnemies.Add(enemy);
                }
            }
            if (inRangeEnemies.Count == 0)
            {
                break;
            }
            target = inRangeEnemies[Random.Range(0, inRangeEnemies.Count)];
            targets.Add(target);
        }

        l.positionCount = targets.Count + 1;
        l.SetPosition(0, transform.position);
        for (int i = 0; i < targets.Count; i++)
        {
            l.SetPosition(i + 1, targets[i].transform.position);
            damageTotal += targets[i].GetComponent<EnemyHealth>().Damage(15);
        }
        Destroy(o, 0.25f);

    }
}
