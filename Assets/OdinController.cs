using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinController : TurretController
{
    public int birdsReady = 2;

    public override bool canShoot()
    {
        return birdsReady > 0;
    }

    public override void bulletSetup(GameObject o, GameObject target)
    {
        print("ddd setting up bullet");
        o.GetComponent<RavenController>().owner = this;
        o.GetComponent<RavenController>().target = target;
        birdsReady--;
    }
}
