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
    public GameObject pauseMenu;

    public bool paused = false;

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
            HexController.spawns.Add(HexController.getNearest(portal.position));
            HexController.computePaths();
        }
        moneyText = mText;
        moneyText.text = "Money: $" + money;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            } else
            {
                Unpause();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            var coroutine = SpawnWave(currentWave);
            StartCoroutine(coroutine);
            text.text = "Round: " + (currentWave + 1);
            currentWave = (currentWave + 1) % waves.Length;
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var pos = startingPos[Random.Range(0, startingPos.Count)];
            var o = Instantiate(enemies[1], pos.position, pos.rotation);
            o.GetComponent<HexMover>().pos = HexController.getNearest(pos.position);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var pos = startingPos[Random.Range(0, startingPos.Count)];
            var o = Instantiate(enemies[2], pos.position, pos.rotation);
            o.GetComponent<HexMover>().pos = HexController.getNearest(pos.position);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var pos = startingPos[Random.Range(0, startingPos.Count)];
            var o = Instantiate(enemies[5], pos.position, pos.rotation);
            o.GetComponent<HexMover>().pos = HexController.getNearest(pos.position);
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
                var pos = startingPos[Random.Range(0, startingPos.Count)];
                var o = Instantiate(enemies[c.type], pos.position, pos.rotation);
                o.GetComponent<HexMover>().pos = HexController.getNearest(pos.position);
            }
        }
    }
}
