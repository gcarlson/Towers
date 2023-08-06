using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : TurretController
{
    public GameObject enemy;
    public GameObject beamPrefab;
    private GameObject beam;
    private float nextTick = 0;
    public float tickRate = 8;

    public override void shoot(GameObject target, Quaternion rotation)
    {
        enemy = target;
        beam = Instantiate(beamPrefab);
        var l = beam.GetComponent<LineRenderer>();
        l.positionCount = 2;
        l.SetPosition(0, transform.position);
        l.SetPosition(1, enemy.transform.position);
    }

    // Update is called once per frame
    public void Update()
    {
        if (enemy)
        {
            var l = beam.GetComponent<LineRenderer>();
            l.positionCount = 2;
            l.SetPosition(0, transform.position);
            l.SetPosition(1, enemy.transform.position);
            
            var targetPos = enemy.transform.position;
            var lookPos = targetPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
                turret.transform.rotation = rotation;
            if (Time.time > nextTick)
            {
                damageTotal += enemy.GetComponent<EnemyHealth>().Damage(5, damageType);
                nextTick = Time.time + 1.0f / tickRate;
            }
        } else
        {
            if (beam)
            {
                Destroy(beam);
            }
            base.Update();
        }
    }
}
