using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexController : MonoBehaviour
{
    public static bool[,] obstacle;
    public static int[,] distance;
    public static int gridWidth = 53;
    public static int gridHeight = 53;
    // Start is called before the first frame update
    void Start()
    {
        print("ddd starting controller");
        obstacle = new bool[gridWidth, gridHeight];
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                obstacle[i, j] = false;
            }
        }
        distance = new int[gridWidth, gridHeight];
    }
    public static Vector2Int getNext(int x, int y)
    {
        int startDistance = distance[x, y];
        List<Vector2Int> opts = new List<Vector2Int>();
        foreach (Vector2Int n in neighbors(x, y))
        {
            if (distance[n.x, n.y] < startDistance && !obstacle[n.x, n.y])
            {
                opts.Add(n);
            }
        }
        return opts[((int) Random.Range(0, 60.0f)) % opts.Count];
    }
    public static Vector3 getPos(Vector2Int p)
    {
        float h = 1.73205080757f;
        return new Vector3((p.x - gridWidth / 2) * 1.5f, 1.0f, (p.y - gridHeight / 2) * h + (p.x % 2 == 0 ? 0 : h / 2));
    }
    public static void addObstacle(int x, int y)
    {
        print("ddd adding obs pos: " + (x + gridWidth / 2) + " " + (y + gridHeight / 2));
        obstacle[x + gridWidth / 2, y + gridHeight / 2] = true;
        computePaths();
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
        distance[gridWidth / 2, gridHeight / 2] = 0;
        boundary.Add(new Vector2Int(gridWidth / 2, gridHeight / 2));

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
