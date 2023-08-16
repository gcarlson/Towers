using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentHealth : EnemyHealth
{
    public EnemyHealth headHealth;
    public override int Damage(int damage, TurretController.Element type)
    {
        print("ddd override damage");
        return headHealth.Damage(damage, type);
    }
}
