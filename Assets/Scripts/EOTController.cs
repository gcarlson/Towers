using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOTController : HomingBulletController
{
    public GameObject submunitions;
    public int subCount = 5;
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

            int num = subCount / hitColliders.Length;
            int bonus = subCount - num * hitColliders.Length;
            for (int i = 0; (num > 0 && i < hitColliders.Length) || i < bonus; i++)
            {
                print("ddd y angle: " + transform.eulerAngles.y);
                for (int j = 0; j < num + (i < bonus ? 1 : 0); j++)
                {
                    var o = Instantiate(submunitions, transform.position, Quaternion.Euler(0.0f, transform.eulerAngles.y + Random.Range(-90, 90), 0.0f));
                    o.GetComponent<HomingBulletController>().target = hitColliders[i].gameObject;
                    o.GetComponent<HomingBulletController>().owner = owner;
                    Destroy(o, 3.0f);
                }
            }
            Destroy(gameObject);
        }
        else
        {
            base.FixedUpdate();
        }
    }
}
