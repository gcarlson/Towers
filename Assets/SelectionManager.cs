using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TurretController selected;
    public GameObject GloryShot;
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
                        GloryShot.transform.position = selected.transform.position;
                    }
                    else
                    {
                        icon.SetActive(false);
                        text.text = "";
                    }
                    var clicked = hit.collider.gameObject.GetComponent<OutpostController>();
                    if (clicked)
                    {
                        //icon.SetActive(true);
                        //GloryShot.transform.position = selected.transform.position;
                        HexController.spawnOutpost(clicked.transform.position);
                    }
                }
            }
        }
        if (selected)
        {
            text.text = "Damage Dealt: " + selected.damageTotal + "";
        }
    }

    void FixedUpdate()
    {
        if (selected)
        {
            GloryShot.transform.Rotate(0, 5 * Time.deltaTime, 0);
        }
    }
}
