using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    public float tickRate = 0.2f;
    private float nextShot;
    public LayerMask m_LayerMask;
    public TurretController owner;

    // Start is called before the first frame update
    void Start()
    {
        nextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShot)
        {
            nextShot += tickRate;

            Collider[] hitColliders = Physics.OverlapCapsule(transform.position - 4.8f * transform.right, transform.position + 5.8f * transform.right, 2.2f, m_LayerMask);
            foreach (var enemy in hitColliders)
            {
                owner.damageTotal += enemy.gameObject.GetComponent<EnemyHealth>().Damage(2);
            }
        }
    }
}
