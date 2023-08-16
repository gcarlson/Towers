using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentUpdater : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public float lastdistance = 100000.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var p = agent.destination;
        var oldPath = agent.path;
        agent.SetDestination(p);
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        agent.CalculatePath(p, path);
        agent.SetPath(path);
        if (agent.remainingDistance > lastdistance)
        {
            agent.SetPath(oldPath);
        }
        lastdistance = agent.remainingDistance;
    }
}
