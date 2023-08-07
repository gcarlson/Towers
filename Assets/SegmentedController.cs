using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentedController : HexMover
{
    public SerpentController serpent;

    public override void Move()
    {
        pos = serpent.getNext(pos);//leader ? leader.GetComponent<HexMover>().pos : HexController.getNext(pos.x, pos.y);
        distance = HexController.distance[pos.x, pos.y];
        transform.position = target;

        visible = (HexController.fogs[pos.x, pos.y] == null);
        if (HPBar)
        {
            HPBar.SetActive(visible);
        }
        GetComponent<Collider>().enabled = visible;
        target = HexController.getPos(pos);
        transform.LookAt(target);
        //transform.position = HexController.getPos(pos);
        print("ddd pos: " + pos.x + " " + pos.y + " " + HexController.distance[pos.x, pos.y]);
    }
}
