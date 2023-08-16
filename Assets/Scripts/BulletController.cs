using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 1;
    public bool destroyOnHit = true;
    public TurretController owner;
    public TurretController.Element damageType;
    public GameObject createOnHit;
    public string impactSound = "";
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
        if (impactSound.Length != 0)
        {
            FindObjectOfType<AudioManager>().Play(impactSound);
        }
        int d = other.gameObject.GetComponent<EnemyHealth>().Damage(damage, damageType);
        owner.damageTotal += (d < 0 ? 0 - d : d);
        if (d < 0)
        {
            owner.kills++;
            OnKill();
        }
        if (createOnHit)
        {
            var o = Instantiate(createOnHit, transform.position, transform.rotation);
            o.GetComponent<BulletController>().owner = owner;
            Destroy(o, 0.5f);
        }

        if (destroyOnHit)
        {
            Destroy(this.gameObject);
        }

    }
}
