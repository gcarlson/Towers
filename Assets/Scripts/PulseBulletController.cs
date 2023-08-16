using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBulletController : BulletController
{
    public GameObject bullet;
    private float speed = 30.0f;

    public override void OnKill()
    {
        print("ddd pulse kill");
        float offset = Random.Range(0.0f, 90.0f);
        for (int i = 0; i < 4; i++)
        {
            var o = Instantiate(bullet, transform.position, transform.rotation);
            var v = o.transform.eulerAngles;
            v.y += (90 * i + offset);
            o.transform.eulerAngles = v;
            o.GetComponent<BulletController>().owner = owner;
            o.GetComponent<Rigidbody>().velocity = (speed) * o.transform.forward.normalized;
            Destroy(o, 0.5f);
        }
    }
}
