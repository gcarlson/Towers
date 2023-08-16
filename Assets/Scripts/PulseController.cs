using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseController : TurretController
{
    public int burstCount = 3;
    public override void shoot(GameObject target, Quaternion rotation)
    {
        StartCoroutine(BurstFire(target, rotation));
    }
    IEnumerator BurstFire(GameObject target, Quaternion rotation)
    {
        base.shoot(target, rotation);
        for (int i = 1; i < burstCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            base.shoot(target, rotation);
        }
    }
}
