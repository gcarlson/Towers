using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMover : MonoBehaviour
{
    public Vector2Int pos;
    public int distance = 100000;
    public float nextMove;
    public Vector3 target;
    public float moveTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        nextMove = Time.time;
        target = HexController.getPos(pos);
    }

    void Move()
    {
        pos = HexController.getNext(pos.x, pos.y);
        distance = HexController.distance[pos.x, pos.y];
        transform.position = target;
        
        target = HexController.getPos(pos);
        transform.LookAt(target);
        //transform.position = HexController.getPos(pos);
        print("ddd pos: " + pos.x + " " + pos.y + " " + HexController.distance[pos.x, pos.y]);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (target - transform.position).normalized * Time.deltaTime / moveTime * 1.73205080757f;
        if (Time.time > nextMove)
        {
            nextMove += moveTime;
            Move();
        }
    }
}
