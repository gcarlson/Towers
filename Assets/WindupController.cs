using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindupController : TurretController
{
    public GameObject enemy;
    public GameObject beamPrefab;
    private GameObject beam;
    private float nextTick = 0;
    public float tickRate = 8;
    public float windup = 1.0f;
    public float baseDamage = 5.0f;
    public float damageScaling = 0.125f;
    public float maxDamage = 20.0f;
    private float windupTime;
    private float damage = 5.0f;


    public override void shoot(GameObject target, Quaternion rotation)
    {
        enemy = target;
        windupTime = Time.time + 1.0f;
    }

    // Update is called once per frame
    public new void Update()
    {
        if (enemy && Vector3.Distance(transform.position, enemy.transform.position) <= range && enemy.GetComponent<HexMover>().visible)
        {
            var targetPos = enemy.transform.position;
            var lookPos = targetPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            turret.transform.rotation = rotation;

            if (Time.time > windupTime)
            {
                if (!beam)
                {
                    beam = Instantiate(beamPrefab);
                    damage = baseDamage;
                }
                if (Time.time > nextTick)
                {
                    int d = enemy.GetComponent<EnemyHealth>().Damage((int)damage, damageType);
                    if (d < 0)
                    {
                        kills++;
                        damageTotal -= d;
                    }
                    else
                    {
                        damageTotal += d;
                    }
                    nextTick = Time.time + 1.0f / tickRate;
                    if (damage < maxDamage)
                    {
                        damage += damageScaling;
                    }
                }
                var l = beam.GetComponent<LineRenderer>();
                l.positionCount = 2;
                l.SetPosition(0, transform.position);
                l.SetPosition(1, enemy.transform.position);
            }
        }
        else
        {
            if (beam)
            {
                Destroy(beam);
            }
            base.Update();
        }
    }
}
