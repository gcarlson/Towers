using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*public new Camera camera;
    public float scrollSpeed = 3.0f;
    public float minSize = 5.0f;
    public float maxSize = 36.0f;
    public float minX = -12.0f;
    public float maxX = 12.0f;
    public float minZ = -10.0f;
    public float maxZ = 10.0f;
    */

    public Transform cameraTransform;

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public float minZoom, maxZoom;
    public float maxX, maxZ;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(HandleMouseInput().mousePosition);
        }*/
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 diff = rotateCurrentPosition - rotateStartPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(0, diff.x / 5.0f, 0);
            newRotation *= Quaternion.Euler(diff.y / -5.0f, 0, 0);
        }

        Vector3 pos = Input.mousePosition;
        print("ddd axis: " + pos.x + " " + Screen.width + " " + pos.y + " " + Screen.height);
        if (pos.x < 0.0f)
        {
            newPosition -= (transform.right * movementSpeed * Time.deltaTime);
        }
        else if (pos.x > Screen.width)
        {
            newPosition += (transform.right * movementSpeed * Time.deltaTime);
        }
        if (pos.y < 0.0f)
        {
            newPosition -= (transform.forward * movementSpeed * Time.deltaTime);
        }
        else if (pos.y > Screen.height)
        {
            newPosition += (transform.forward * movementSpeed * Time.deltaTime);
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition -= (transform.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition -= (transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.down * rotationAmount * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        float zoomSlide = 0.1f * newZoom.y;
        newPosition.y = 0;
        newPosition.x = Mathf.Clamp(newPosition.x, -maxX + zoomSlide, maxX - zoomSlide);
        newPosition.z = Mathf.Clamp(newPosition.z, -maxZ + zoomSlide, maxZ - zoomSlide);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        float xrot = newRotation.eulerAngles.x > 180 ? newRotation.eulerAngles.x - 360 : newRotation.eulerAngles.x;
        newRotation = Quaternion.Euler(Mathf.Clamp(xrot, -40, 40), newRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);

        if (newZoom.y < minZoom)
        {
            newZoom.y = minZoom;
            newZoom.z = -minZoom;
        }
        if (newZoom.y > maxZoom)
        {
            newZoom.y = maxZoom;
            newZoom.z = -maxZoom;
        }
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    /*void FixedUpdate()
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
    }*/
}
