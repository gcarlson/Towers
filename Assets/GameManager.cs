using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject turret1, turret2, turret3, turret4, turret5, enemy;

    public Transform startingPos;
    public Transform endingPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(turret1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(turret2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(turret3);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(turret4);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(turret5);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var o = Instantiate(enemy, startingPos.position, startingPos.rotation);
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
        }
    }
}
