using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadController : TurretController
{
    public override void shoot(GameObject target, Quaternion rotation)
    {
        for (int i = 0; i < multishot; i++)
        {
            var o = Instantiate(bullet, transform.position, rotation);
            var v = o.transform.eulerAngles;
            v.y += spread * (i - (multishot - 1) / 2.0f);
            o.transform.eulerAngles = v;
            o.GetComponent<HomingBulletController>().owner = this;
            o.GetComponent<Rigidbody>().velocity = (speed + Random.Range(0 - speedRange, speedRange)) * o.transform.forward.normalized;
            o.GetComponent<HomingBulletController>().target = target;
            Destroy(o, lifespan);
        }
    }
}
