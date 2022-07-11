using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class GooseLadyWander : Action
{
    public float radius;
    public Vector3 center;
    public SharedFloat idleTimer;
    public SharedVector3 destination;
    private NavMeshAgent agent;
    private Vector3 wanderTarget;
    private bool isIdle;


    public override void OnAwake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("Goose lady have no navmesh agent.");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (agent == null)
        {
            return TaskStatus.Failure;
        }
        if (idleTimer.Value > 0)
        {
            idleTimer.Value -= Time.deltaTime;
        }
        else 
        {
            
        }
        return TaskStatus.Success;
    }

    //private Vector3 GetRandomWanderTarget()
    //{
        
    //}
}
