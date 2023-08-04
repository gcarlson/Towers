using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 1;
    public bool destroyOnHit = true;
    public TurretController owner; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnKill()
    {
        print("ddd base kill");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ddd bullet collided");
        int d = other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        owner.damageTotal += (d < 0 ? 0 - d : d);
        if (d < 0)
        {
            OnKill();
        }

            if (destroyOnHit)
            {
                Destroy(this.gameObject);
            }
        
    }
}
