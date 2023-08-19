using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOTController : HomingBulletController
{
    public GameObject submunitions;
    public float triggerRange = 5.0f;
    public float minSplit = 5.0f;
    public LayerMask m_LayerMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (target && Vector3.Distance(target.transform.position, gameObject.transform.position) < triggerRange && Vector3.Distance(owner.transform.position, gameObject.transform.position) > minSplit)
        {
            Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, 3.0f, m_LayerMask);
            foreach (var enemy in hitColliders)
            {
                var o = Instantiate(submunitions, transform.position, Quaternion.EulerAngles(0.0f, Random.Range(0, 90), 0.0f));
                o.GetComponent<HomingBulletController>().target = enemy.gameObject;
                o.GetComponent<HomingBulletController>().owner = owner;
                Destroy(o, 3.0f);
            }
            Destroy(gameObject);
        } else
        {
            base.FixedUpdate();
        }
    }
}
