using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretPlacer : MonoBehaviour
{
    private int collisions = 0;
    public int value = 5;
    private Color initialColor;
    private bool canPlace = false;
    private int x = 0;
    private int y = 0;
    public GameObject footprint;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = footprint.GetComponentInChildren<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        var v3 = Input.mousePosition;
        v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6));
           float h = 1.73205080757f;
            x = (int) (v3.x * 2.0f / 3.0f + 0.5f);
        if (x % 2 == 0)
        {
            y = (int)(v3.z / h + 0.5f);
        }
        else
        {
            y = (int)((v3.z - h / 2.0f) / h + 0.5f);
        }
            transform.position = new Vector3(x * 1.5f, 0, y * h + (x % 2 == 0 ? 0 : h / 2));
        if (HexController.isObstructed(x, y))
        {
            canPlace = false;
            foreach (Renderer r in footprint.GetComponentsInChildren<Renderer>())
            {
                r.material.SetColor("_Color", Color.red);
            }
        }
        else
        {
            canPlace = true;
            foreach (Renderer r in footprint.GetComponentsInChildren<Renderer>())
            {
                r.material.SetColor("_Color", initialColor);
            }
        }
        
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            GameManager.AddMoney(0 - value);
            HexController.addObstacle(x, y);
            this.enabled = false;
        }
    }
}
