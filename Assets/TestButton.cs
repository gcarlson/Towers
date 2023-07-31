using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestButton : MonoBehaviour
{
    public GameObject turret;
    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Instantiate(turret);
        }
    }

    public void click()
    {
        print("ddd clicked");
        Instantiate(turret);
    }
}
