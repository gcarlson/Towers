using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexController : MonoBehaviour
{
    public static bool[,] obstacle;
    public static int[,] distance;
    public static GameObject[,] fogs;
    public static int gridWidth = 53;
    public static int gridHeight = 53;
    public GameObject fog;
    public static GameObject fogPrefab;
    public GameObject path;
    public static GameObject pathPrefab;
    private static List<Vector2Int> bases;
    public static List<Vector2Int> spawns = new List<Vector2Int>();
    
    public GameObject mainBase;
    public static GameObject[,] paths;
    public static bool heatmapActive = true;

    // Start is called before the first frame update
    void Start()
    {
        print("ddd starting controller");
        fogPrefab = fog;
        pathPrefab = path;
        bases = new List<Vector2Int>();
        //bases.Add(new Vector2Int(gridWidth / 2, gridHeight / 2));
        bases.Add(getNearest(mainBase.transform.position));
        obstacle = new bool[gridWidth, gridHeight];
        fogs = new GameObject[gridWidth, gridHeight];
        paths = new GameObject[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                obstacle[i, j] = false;
            }
        }
        distance = new int[gridWidth, gridHeight];
        computePaths();
        for (int i = 35; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                var v = getPos(new Vector2Int(i, j));
                v.y = 5.0f;
                fogs[i, j] = Instantiate(fog, v, Quaternion.identity);
            }
        }
    }
    public static Vector2Int getNext(int x, int y)
    {
        int startDistance = distance[x, y];
        List<Vector2Int> opts = new List<Vector2Int>();
        foreach (Vector2Int n in neighbors(x, y))
        {
            if (distance[n.x, n.y] == 0 || (distance[n.x, n.y] < startDistance && !obstacle[n.x, n.y]))
            {
                opts.Add(n);
            }
        }
        return opts[((int) Random.Range(0, 60.0f)) % opts.Count];
    }
    public static void spawnOutpost(Vector3 p)
    {
        Vector2Int v = getNearest(p);
        for (int i = 35; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Destroy(fogs[i, j]);
            }
        }

        bases.Add(v);
        obstacle[v.x, v.y] = true;
        foreach (Vector2Int w in neighbors(v.x, v.y))
        {
            obstacle[w.x, w.y] = true;
        }
        computePaths();
    }

    public static void destroyOutpost(Vector3 p)
    {
        Vector2Int v = getNearest(p);

        for (int i = 35; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                var w = getPos(new Vector2Int(i, j));
                w.y = 5.0f;
                fogs[i, j] = Instantiate(fogPrefab, w, Quaternion.identity);
            }
        }

        bases.Remove(v);
        obstacle[v.x, v.y] = false;
        foreach (Vector2Int w in neighbors(v.x, v.y))
        {
            obstacle[w.x, w.y] = false;
        }
        computePaths();
    }


    public static Vector2Int getNearest(Vector3 p)
    {
        float h = 1.73205080757f;
        int x = Mathf.RoundToInt(p.x * 2.0f / 3.0f);
        if (x % 2 == 0)
        {
            return new Vector2Int(x + gridWidth / 2, Mathf.RoundToInt(p.z / h) + gridHeight / 2);
        }
        else
        {
            return new Vector2Int(x + gridWidth / 2, Mathf.RoundToInt((p.z - h / 2.0f) / h) + gridHeight / 2);
        }
    }
    public static Vector3 getPos(Vector2Int p)
    {
        float h = 1.73205080757f;
        return new Vector3((p.x - gridWidth / 2) * 1.5f, 0.0f, (p.y - gridHeight / 2) * h + (p.x % 2 == 0 ? 0 : h / 2));
    }
    public static void addObstacle(int x, int y)
    {
        print("ddd adding obs pos: " + (x + gridWidth / 2) + " " + (y + gridHeight / 2));
        obstacle[x + gridWidth / 2, y + gridHeight / 2] = true;
        computePaths();
    }
    public static void removeObstacle(int x, int y)
    {
        print("ddd removing obs pos: " + (x + gridWidth / 2) + " " + (y + gridHeight / 2));
        obstacle[x + gridWidth / 2, y + gridHeight / 2] = false;
        computePaths();
    }

    public static bool isObstructed(int x, int y)
    {
        return obstacle[x + gridWidth / 2, y + gridHeight / 2] || fogs[x + gridWidth / 2, y + gridHeight / 2] != null;
    }

    public static List<Vector2Int> neighbors(int x, int y)
    {
        List<Vector2Int> l = new List<Vector2Int>();
        if (y + 1 < gridHeight)
        {
            l.Add(new Vector2Int(x, y + 1));
        }
        if (y > 0)
        {
            l.Add(new Vector2Int(x, y - 1));
        }
        if (x + 1 < gridWidth)
        {
            l.Add(new Vector2Int(x + 1, y));
            if (x % 2 == 0)
            {
                if (y > 0)
                {
                    l.Add(new Vector2Int(x + 1, y - 1));
                }
            }
            else
            {
                if (y + 1 < gridHeight)
                {
                    l.Add(new Vector2Int(x + 1, y + 1));
                }
            }
        }
        if (x > 0)
        {
            l.Add(new Vector2Int(x - 1, y));
            if (x % 2 == 0)
            {
                if (y > 0)
                {
                    l.Add(new Vector2Int(x - 1, y - 1));
                }
            }
            else
            {
                if (y + 1 < gridHeight)
                {
                    l.Add(new Vector2Int(x - 1, y + 1));
                }
            }
        }
        return l;
    }

    public static void computePaths()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                distance[i, j] = -1;
            }
        }
        List<Vector2Int> boundary = new List<Vector2Int>();

        foreach (Vector2Int v in bases)
        {
            print("ddd base: " + v.x + " " + v.y);
            foreach (Vector2Int n in neighbors(v.x, v.y))
            {
                distance[n.x, n.y] = 0;
                boundary.Add(n);
            }
        }

        for (int i = 1;  boundary.Count > 0; i++)
        {
            print("ddd search depth: " + i + " boundary: " + boundary.Count);
            List<Vector2Int> newBoundary = new List<Vector2Int>();
            foreach (Vector2Int v in boundary)
            {
                foreach (Vector2Int n in neighbors(v.x, v.y))
                {
                    if (!obstacle[n.x, n.y] && distance[n.x, n.y] < 0)
                    {
                        distance[n.x, n.y] = i;
                        newBoundary.Add(n);
                    }
                }
            }
            boundary = newBoundary;
        }
        compute();

    }

    public static void compute()
    {
        print("ddd computing " + spawns.Count);
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (paths[i, j])
                {
                    Destroy(paths[i, j]);
                    paths[i, j] = null;
                }
            }
        }
        List<Vector2Int> boundary = spawns;

        for (int i = 1; boundary.Count > 0; i++)
        {
            print("ddd computing round: " + i);
            //print("ddd search depth: " + i + " boundary: " + boundary.Count);
            List<Vector2Int> newBoundary = new List<Vector2Int>();
            foreach (Vector2Int v in boundary)
            {
                foreach (Vector2Int n in neighbors(v.x, v.y))
                {
                    if (!obstacle[n.x, n.y] && distance[n.x, n.y] < distance[v.x, v.y] && !paths[n.x, n.y])
                    {
                        newBoundary.Add(n);
                        var vv = getPos(n);
                        vv.y = 0.0f;
                        paths[n.x, n.y] = Instantiate(pathPrefab, vv, Quaternion.identity);
                        paths[n.x, n.y].SetActive(heatmapActive);
                        print("ddd color: " + (50 - 5 * distance[n.x, n.y]));
                        foreach (var o in paths[n.x, n.y].GetComponentsInChildren<Renderer>())
                        {
                            
                            o.material.color = new Color(1.0f - distance[n.x, n.y] / 20.0f, 0, 0);
                        }
                    }
                }
            }
            boundary = newBoundary;
        }
    }

    public void SetHeatmapActive(bool active)
    {
        heatmapActive = active;
        foreach (GameObject o in paths)
        {
            if (o)
            {
                o.SetActive(active);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
