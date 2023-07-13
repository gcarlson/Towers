using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TurretController selected;
    public Camera GloryShot;
    public GameObject icon;

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
            icon.SetActive(false);
            text.text = "";
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
                    if (selected)
                    {
                        icon.SetActive(true);
                        GloryShot.transform.position = selected.transform.position + new Vector3(4, 4, 4);
                    }
                    else
                    {
                        icon.SetActive(false);
                        text.text = "";
                    }
                }
            }
        }
        if (selected)
        {
            text.text = "Damage Dealt: " + selected.damageTotal + "";
         } 
    }
}
