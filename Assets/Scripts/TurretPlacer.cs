using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretPlacer : MonoBehaviour
{
    public int value = 5;
    private Color initialColor;
    private bool canPlace = false;
    private int x = 0;
    private int y = 0;
    public GameObject footprint;
    public Vector2Int[] hexes = { new Vector2Int(0, 0) };
    public Vector3 centerOffset = new Vector3(0, 0, 0);
    private LayerMask layerMask = 1783;

    // Start is called before the first frame update
    void Start()
    {
        print("ddd starting " + layerMask.value);
        initialColor = footprint.GetComponentInChildren<Renderer>().material.color;
    }

    Vector3Int oddq_to_cube(Vector2Int hex)
    {
        int q = hex.x;
        int r = hex.y - (hex.x - (hex.x & 1)) / 2;
        return new Vector3Int(q, r, 0 - q - r);
    }

    Vector2Int cube_to_oddq(Vector3Int cube)
    {
        int q = cube.x;
        int r = cube.y + (cube.x - (cube.x & 1)) / 2;
        return new Vector2Int(q, r);
    }

    bool Obstructed()
    {
        foreach (Vector2Int v in hexes)
        {
            if (HexController.isObstructed(x + v.x, y + v.y + (v.x % 2 != 0 && (v.x + x) % 2 == 0 ? 1 : 0)))
            {
                return true;
            }
        }
        return false;
    }

    public void Delete()
    {
        List<Vector2Int> l = new List<Vector2Int>();
        foreach (Vector2Int v in hexes)
        {
            l.Add(new Vector2Int(x + v.x, y + v.y + (v.x % 2 != 0 && (v.x + x) % 2 == 0 ? 1 : 0)));
        }
        HexController.removeObstacles(l);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            centerOffset = Quaternion.AngleAxis(60f, Vector3.forward) * centerOffset;
            for (int i = 0; i < hexes.Length; i++)
            {
                var res = oddq_to_cube(hexes[i]);
                hexes[i] = cube_to_oddq(new Vector3Int(res.y, res.z, res.x) * -1);
            }
            transform.Rotate(0, -60, 0);
            print("ddd offset " + centerOffset);
        }
        //var v3 = Input.mousePosition;
        //print("ddd pos: " + Input.mousePosition);
        //v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 60));

        Vector3 v3 = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask = layerMask.value))
        {
            //            Debug.Log(hit.transform.name);
            Debug.Log("ddd hit");
            v3 = hit.point;
            v3.y = 0;
        }

        print("ddd hover: " + v3);
        float h = 1.73205080757f;
        v3.x -= centerOffset.x;
        v3.z -= centerOffset.y;
        x = Mathf.RoundToInt(v3.x * 2.0f / 3.0f);
        if (x % 2 == 0)
        {
            y = Mathf.RoundToInt(v3.z / h);
        }
        else
        {
            y = Mathf.RoundToInt((v3.z - h / 2.0f) / h);
        }
        transform.position = new Vector3(x * 1.5f + centerOffset.x, 0, y * h + (x % 2 == 0 ? 0 : h / 2) + centerOffset.y);
        if (Obstructed())
        {
            canPlace = false;
            footprint.transform.localPosition = new Vector3(0, 0.1f, 0);
            foreach (Renderer r in footprint.GetComponentsInChildren<Renderer>())
            {
                r.material.SetColor("_Color", Color.red);
            }
        }
        else
        {
            canPlace = true;
            footprint.transform.localPosition = new Vector3(0, 0, 0);

            foreach (Renderer r in footprint.GetComponentsInChildren<Renderer>())
            {
                r.material.SetColor("_Color", initialColor);
            }
        }

        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            GameManager.AddMoney(0 - value);
            List<Vector2Int> l = new List<Vector2Int>();
            foreach (Vector2Int v in hexes)
            {
                l.Add(new Vector2Int(x + v.x, y + v.y + (v.x % 2 != 0 && (v.x + x) % 2 == 0 ? 1 : 0)));
            }
            HexController.addObstacles(l);
            this.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Destroy(gameObject);
        }
    }
}