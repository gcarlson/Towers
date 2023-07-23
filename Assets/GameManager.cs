using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    public GameObject turret1, turret2, turret3, turret4, turret5, turret6, turret7;
    public GameObject[] enemies;
    public Wave[] waves;
    public static List<Transform> startingPos;
    public static int money = 100;
    public TextMeshProUGUI mText;
    public static TextMeshProUGUI moneyText;
    public Transform endingPos;
    public int currentWave = 0;
    public TextMeshProUGUI text;
    public Transform[] portals;

    public static void AddMoney(int cash)
    {
        money += cash;
        moneyText.text = "Money: $" + money;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPos = new List<Transform>();
        foreach (Transform portal in portals)
        {
            startingPos.Add(portal);
        }
        moneyText = mText;
        moneyText.text = "Money: $" + money;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            var coroutine = SpawnWave(currentWave);
            StartCoroutine(coroutine);
            text.text = "Round: " + (currentWave + 1);
            currentWave = (currentWave + 1) % waves.Length;
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var o = Instantiate(enemies[0], startingPos[Random.Range(0, startingPos.Count)].position, startingPos[0].rotation);
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var p = startingPos[Random.Range(0, startingPos.Count)].position;
            p.y = 0.4333333f;
            var o = Instantiate(enemies[1], p, Quaternion.LookRotation(new Vector3(endingPos.position.x - p.x, 0.0f, endingPos.position.z - p.z).normalized));
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(endingPos.position);
            UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().CalculatePath(endingPos.position, path);

            o.GetComponent<UnityEngine.AI.NavMeshAgent>().SetPath(path);
            print("ddd remaining distance: " + o.GetComponent<UnityEngine.AI.NavMeshAgent>().pathStatus + " " + PathDistance(path));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            var o = Instantiate(enemies[2], startingPos[Random.Range(0, startingPos.Count)].position, startingPos[0].rotation);
            o.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            var pos = startingPos[Random.Range(0, startingPos.Count)];
            var o = Instantiate(enemies[3], pos.position, pos.rotation);
            o.GetComponent<HexMover>().pos = HexController.getNearest(pos.position);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            var pos = startingPos[Random.Range(0, startingPos.Count)];
            var o = Instantiate(enemies[4], pos.position, pos.rotation);
            o.GetComponent<HexMover>().pos = HexController.getNearest(pos.position);
        }


    }

    private float PathDistance(UnityEngine.AI.NavMeshPath path)
    {
        float distance = 0.0f;
        for (int i = 0; i < path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }

    private IEnumerator SpawnWave(int number)
    {
        foreach (Cluster c in waves[number].clusters)
        {
            for (int i = 0; i < c.number; i++)
            {
                yield return new WaitForSeconds(i == 0 ? c.startDelay : c.spacing);
                var p = Instantiate(enemies[c.type], startingPos[Random.Range(0, startingPos.Count)].position, startingPos[0].rotation);
                p.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = endingPos.position;
            }
        }
    }
}
