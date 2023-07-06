using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmController : MonoBehaviour
{
    public float tickRate = 0.2f;
    private float nextShot;
    public LayerMask m_LayerMask;

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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f, m_LayerMask);
            foreach (var enemy in hitColliders)
            {
                enemy.gameObject.GetComponent<EnemyHealth>().Damage(2);
            }
        }
    }
}
