using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentController : MonoBehaviour
{
    public int segments = 20;
    public float segmentDelay = 1.4f;
    private Dictionary<Vector2Int, Vector2Int> d = new Dictionary<Vector2Int, Vector2Int>();
    public GameObject segment;
    private List<GameObject> segmentList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var coroutine = SpawnSnake();
        StartCoroutine(coroutine);
    }

    public Vector2Int getNext(Vector2Int pos) {
        if (!d.ContainsKey(pos))
        {
            d.Add(pos, HexController.getNext(pos.x, pos.y));
        }
        return d[pos];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        foreach (GameObject o in segmentList)
        {
            Destroy(o);
        }
    }

    private IEnumerator SpawnSnake()
    {
        var p = transform.position;
        var r = transform.rotation;
        for (int i = 0; i < segments; i++)
        {
            yield return new WaitForSeconds(segmentDelay);
            var o = Instantiate(segment, p, r);
            segmentList.Add(o);
            o.GetComponent<SegmentedController>().pos = HexController.getNearest(p);
            o.GetComponent<SegmentedController>().serpent = this;
            o.GetComponent<SegmentHealth>().headHealth = GetComponent<EnemyHealth>();
        }
    }
}
