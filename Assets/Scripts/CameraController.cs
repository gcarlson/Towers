using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public new Camera camera;
    public float scrollSpeed = 3.0f;
    public float minSize = 5.0f;
    public float maxSize = 36.0f;
    public float minX = -12.0f;
    public float maxX = 12.0f;
    public float minZ = -10.0f;
    public float maxZ = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - 4 * Input.GetAxis("Mouse ScrollWheel"), minSize, maxSize);
        Vector3 pos = Input.mousePosition;
        print("ddd axis: " + pos.x + " " + Screen.width+ " " + pos.y + " " + Screen.height);
        if (pos.x < 0.0f)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - scrollSpeed * Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
        }
        else if (pos.x > Screen.width)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + scrollSpeed * Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
        }
        if (pos.y < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - scrollSpeed * Time.deltaTime, minZ, maxZ));
        }
        else if (pos.y > Screen.height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + scrollSpeed * Time.deltaTime, minZ, maxZ));
        }
    }
}
