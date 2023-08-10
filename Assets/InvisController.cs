using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisController : EnemyHealth
{
    public Material decloakMat, cloakMat;
    public GameObject model;

    public override int Damage(int damage, TurretController.Element type)
    {
        int oldHP = hp;
        int res = base.Damage(damage, type);
        if (hp < maxHp / 2 && oldHP >= maxHp / 2)
        {
            var coroutine = Cloak();
            StartCoroutine(coroutine);
        }
        return res;
    }

    private IEnumerator Cloak()
    {
        model.GetComponent<Renderer>().material = cloakMat;
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(5.0f);
        model.GetComponent<Renderer>().material = decloakMat;
        gameObject.tag = "Enemy";

    }
}