using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TurretController selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(selected.gameObject);
            selected = null;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    selected = hit.collider.gameObject.GetComponent<TurretController>();
                }
                else
                {
                    selected = null;
                }

            }
        }
        if (selected)
        {
            text.text = selected.damageTotal + "";
        } else
        {
            text.text = "";
        }

    }
}
