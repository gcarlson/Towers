using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public float fireRate = 0.5f;
    public GameObject bullet;
    private float nextShot = 0.0f;
    public Transform endingPos;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("MainCamera");
        gm = enemies[0].GetComponent<GameManager>();
        nextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.time > nextShot)
            {
                nextShot += 1.0f / fireRate;
                var o = Instantiate(bullet, transform.position, transform.rotation);
            o.GetComponent<BulletController>().owner = GetComponent<TurretController>();
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = GameManager.startingPos[Random.Range(0, GameManager.startingPos.Count)].position;//new Vector3(-30.0f, 0.0f, -10.0f);
            }
    }
}
