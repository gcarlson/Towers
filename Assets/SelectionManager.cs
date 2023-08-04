using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TurretController selected;
    public GameObject turretBar;
    public GameObject GloryShot;
    public GameObject icon;
    public Dropdown dropdown;
    public TMPro.TMP_Dropdown dd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameManager.AddMoney(selected.gameObject.GetComponent<TurretPlacer>().value / 2);
            selected.gameObject.GetComponent<TurretPlacer>().Delete();
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
                    var s = hit.collider.gameObject.GetComponent<TurretController>();
                    if (s) { selected = s; }
//                    selected = hit.collider.gameObject.GetComponent<TurretController>();
                    if (selected)
                    {
                        dd.value = GetDropDownIndex(selected.priority);
                        turretBar.SetActive(true);
                        //icon.SetActive(true);
                        GloryShot.transform.position = selected.transform.position;
                    }
                    else
                    {
                        //turretBar.SetActive(false);
                        //icon.SetActive(false);
                        //text.text = "";
                    }
                    var clicked = hit.collider.gameObject.GetComponent<OutpostController>();
                    if (clicked)
                    {
                        //icon.SetActive(true);
                        //GloryShot.transform.position = selected.transform.position;
                        HexController.spawnOutpost(clicked.transform.position);
                        clicked.EnableOutpost();
                    }
                }
            }
        }
        if (selected)
        {
            text.text = "Damage Dealt: " + selected.damageTotal + "";
        }
    }

    public void SetPriority(int priority)
    {
        print("ddd prio: " + priority);
        if (selected)
        {
            switch (priority)
            {
                case 0:
                    print("ddd prio first");
                    selected.priority = TurretController.Priority.FIRST;
                    break;
                case 2:
                    print("ddd prio strong");
                    selected.priority = TurretController.Priority.STRONG;
                    break;
                case 3:
                    print("ddd prio weak");
                    selected.priority = TurretController.Priority.WEAK;
                    break;
                case 4:
                    print("ddd prio close");
                    selected.priority = TurretController.Priority.CLOSE;
                    break;
                case 5:
                default:
                    print("ddd prio random");
                    selected.priority = TurretController.Priority.RANDOM;
                    break;
            }
        }
    }

    int GetDropDownIndex(TurretController.Priority priority)
    {
        return (new[] { 0, 4, 2, 5, 3 })[(int) priority];
    }

    void FixedUpdate()
    {
        if (selected)
        {
            GloryShot.transform.Rotate(0, 5 * Time.deltaTime, 0);
        }
    }
}
