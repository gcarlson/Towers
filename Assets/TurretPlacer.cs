using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretPlacer : MonoBehaviour
{
    public bool hex = false;
    private int collisions = 0;
    public int value = 5;
    private Renderer cubeRenderer;
    private Color initialColor;
    public int width, height;
    private bool canPlace = false;
    private int x = 0;
    private int y = 0;

    public LayerMask m_LayerMask;
    // Start is called before the first frame update
    void Start()
    {
         cubeRenderer = gameObject.GetComponent<Renderer>();
    initialColor = cubeRenderer.material.color;
}



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (canPlace || hex))
        {
            GameManager.AddMoney(0 - value);
            HexController.addObstacle(x, y);
            this.enabled = false;
        }
        var v3 = Input.mousePosition;
        v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6));

        if (hex)
        {
            float h = 1.73205080757f;
            x = (int) (v3.x * 2.0f / 3.0f + 0.5f);
            if (x % 2 == 0)
            {
                y = (int) (v3.z / h + 0.5f);
            } else
            {
                y = (int) ((v3.z - h / 2.0f) / h + 0.5f);
            }
            transform.position = new Vector3(x * 1.5f, 0, y * h + (x % 2 == 0 ? 0 : h / 2));
        }
        else
        {
            if (width % 2 == 1)
            {
                v3.x = Mathf.Round(v3.x);
            }
            else
            {
                v3.x = Mathf.Round(v3.x - 0.5f) + 0.5f;
            }
            if (height % 2 == 1)
            {
                v3.z = Mathf.Round(v3.z);
            }
            else
            {
                v3.z = Mathf.Round(v3.z - 0.5f) + 0.5f;
            }

            //print("ddd updating " + v3.x + " " + v3.y + " " + v3.z);
            v3.y = 0.0f;
            transform.position = v3;

            //Use the OverlapBox to detect if there are any other colliders within this box area.
            //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
            Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(width - 0.1f, 1.0f, height - 0.1f) / 2.0f, Quaternion.identity, m_LayerMask);
            print("ddd collisions: " + hitColliders.Length);
            if (hitColliders.Length > 1)
            {
                canPlace = false;
                cubeRenderer.material.SetColor("_Color", Color.red);
            }
            else
            {
                canPlace = true;
                cubeRenderer.material.SetColor("_Color", initialColor);
            }
        }
    }
}
