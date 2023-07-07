using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 1;
    public TurretController owner; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ddd bullet collided");
        owner.damageTotal += other.gameObject.GetComponent<EnemyHealth>().Damage(damage);

        Destroy(this.gameObject);
    }
}
