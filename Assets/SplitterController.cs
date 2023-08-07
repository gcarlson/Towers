using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterController : EnemyHealth
{
    public GameObject spawn;
    public int spawnCount = 2;
    public float spawnSpacing = 0.3f;

    public override void OnKill()
    {
        print("ddd onkill()");
        var coroutine = SpawnWave();
        StartCoroutine(coroutine);
    }
    private IEnumerator SpawnWave()
    {
        var p = transform.position;
        var r = transform.rotation;
        for (int i = 0; i < spawnCount; i++)
        {
            if (i < 0)
            {
                yield return new WaitForSeconds(spawnSpacing);
            }
            print("ddd spawning");
            var o = Instantiate(spawn, p, r);
            o.GetComponent<HexMover>().pos = HexController.getNearest(p);
        }
    }
}
