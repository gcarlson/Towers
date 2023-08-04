using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSelecter : MonoBehaviour
{
    public int selected = 0;
    public TurretController[] ammos;
    public string[] names;

    public void SetAmmo(int ammo)
    {
        ammos[selected].enabled = false;
        selected = ammo;
        ammos[selected].enabled = true;
    }

    public TurretController GetTurret()
    {
        return ammos[selected];
    }

}
