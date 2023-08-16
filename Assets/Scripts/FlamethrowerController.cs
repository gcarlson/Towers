using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : TurretController
{
    public GameObject flame;
    public GameObject enemy;
    private GameObject beam;
    public float tickRate = 8;

    public override void shoot(GameObject target, Quaternion rotation)
    {
        enemy = target;
        if (!beam)
        {
            beam = Instantiate(flame, turret.transform);
            beam.GetComponentInChildren<FlameController>().owner = this;
            beam.transform.parent = turret.transform;
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (Time.time > nextShot && canShoot())
        {
            Destroy(beam);
        }
    }
}
