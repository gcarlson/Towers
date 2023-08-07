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
    private new SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        nextMove = Time.time;
        target = HexController.getPos(pos);
        collider = GetComponent<SphereCollider>();
        visible = (HexController.fogs[pos.x, pos.y] == null);
        HPBar.SetActive(visible);
        collider.enabled = visible;
    }

    void Move()
    {
        pos = HexController.getNext(pos.x, pos.y);
        distance = HexController.distance[pos.x, pos.y];
        transform.position = target;

        visible = (HexController.fogs[pos.x, pos.y] == null);
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
        transform.position += (target - transform.position).normalized * Time.deltaTime * (visible ? moveSpeed : 4.0f) * 1.73205080757f;
        if (Time.time > nextMove)
        {            
            Move();
            nextMove += visible ? 1.0f / moveSpeed : 0.25f;
        }
    }
}
