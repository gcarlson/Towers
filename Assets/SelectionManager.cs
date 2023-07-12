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
            icon.SetActive(true);
            text.text = "Damage Dealt: " + selected.damageTotal + "";
            //-20, 181, -38, 76
            GloryShot.transform.SetPositionAndRotation(new Vector3(selected.transform.position.x + 4, selected.transform.position.y + 4, selected.transform.position.z + 4), new Quaternion(-20,181,-38,-76));
        } else
        {
            icon.SetActive(false);
            text.text = "";
        }

    }
}
