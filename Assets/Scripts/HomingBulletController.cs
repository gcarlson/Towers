using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletController : BulletController
{
    public GameObject target;
    public Rigidbody rigidBody;
    public float angleChangingSpeed;
    public float movementSpeed;
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 direction = target.transform.position - rigidBody.position;
            direction.Normalize();
            Vector3 rotateAmount = (Vector3.Cross(direction, transform.forward));
            rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
            rigidBody.velocity = transform.forward * movementSpeed;
        }
    }
}
