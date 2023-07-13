using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Cluster
    {
        public float startDelay, spacing;
        public int type;
        public int number;
    }

    [System.Serializable]
    public class Wave
    {
        public Cluster[] clusters;
    }


    public GameObject turret1, turret2, turret3, turret4, turret5, turret6, enemy1, enemy2;
    
    public Wave[] waves;
    public Transform[] startingPos;
    public Transform endingPos;
    public int currentWave = 0;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(turret6);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            var coroutine = SpawnWave(currentWave);
            StartCoroutine(coroutine);
            currentWave = (currentWave + 1) % waves.Length;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var o = Instantiate(enemy1, startingPos[Random.Range(0, startingPos.Length)].position, startingPos[0].rotation);
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var o = Instantiate(enemy2, startingPos[Random.Range(0, startingPos.Length)].position, startingPos[0].rotation);
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
        }

    }

    private IEnumerator SpawnWave(int number)
    {
        foreach (Cluster c in waves[number].clusters)
        {
            yield return new WaitForSeconds(c.startDelay);
            var o = Instantiate(c.type == 0 ? enemy1 : enemy2, startingPos[Random.Range(0, startingPos.Length)].position, startingPos[0].rotation);
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;

            for (int i = 1; i < c.number; i++)
            {
                yield return new WaitForSeconds(c.spacing);
                var p = Instantiate(c.type == 0 ? enemy1 : enemy2, startingPos[Random.Range(0, startingPos.Length)].position, startingPos[0].rotation);
                p.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
            }
        }
    }
}
