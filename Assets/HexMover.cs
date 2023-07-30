using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMover : MonoBehaviour
{
    public Vector2Int pos;
    public bool visible = false;
    public int distance = 1000;
    public float nextMove;
    public Vector3 target;
    public float moveSpeed = 1.0f;
    public GameObject HPBar;
    private SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        nextMove = Time.time;
        target = HexController.getPos(pos);
        collider = GetComponent<SphereCollider>();
        visible = HexController.visible[pos.x, pos.y];
        HPBar.SetActive(visible);
        collider.enabled = visible;
    }

    void Move()
    {
        pos = HexController.getNext(pos.x, pos.y);
        distance = HexController.distance[pos.x, pos.y];
        transform.position = target;

        visible = HexController.visible[pos.x, pos.y];
        HPBar.SetActive(visible);
        collider.enabled = visible;
        target = HexController.getPos(pos);
        transform.LookAt(target);
        //transform.position = HexController.getPos(pos);
        print("ddd pos: " + pos.x + " " + pos.y + " " + HexController.distance[pos.x, pos.y]);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (target - transform.position).normalized * Time.deltaTime * moveSpeed * 1.73205080757f;
        if (Time.time > nextMove)
        {
            nextMove += 1.0f / moveSpeed;
            Move();
        }
    }
}
