using Project.Build.Commands;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GooseLadyController : MonoBehaviour
{
    //the following fields are redundent with properties of the same name
    //fields enable designers to edit the value
    //property passes value to design tree
    public Vector3 destination;
    public float playerFollowOffset = 2;
    public float attackRange = 1;
    public float margin = 0.05f;
    public float animationMultiplier = 4.5f;
    [ReadOnly]
    public float targetDistance;
    public bool IsInCombat { get; set; }
    public float TargetDistance
    {
        get { return targetDistance; }
        set { targetDistance = value; }
    }
    public Vector3 Destination
    {
        get { return destination; }
        set { destination = value; }
    }
    public float PlayerFollowOffset
    {
        get { return playerFollowOffset; }
        set { playerFollowOffset = value; }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    [ReadOnly]
    public Transform target;
    public Vector3 resetOffset;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        target = player;
    }

    private void Update()
    {
        Vector3 directionVector = (transform.position - target.position).normalized;
        if (target.TryGetComponent<Interactable>(out Interactable targetInteractable))
        {
            Destination = directionVector * (attackRange + targetInteractable.radius + agent.radius - margin) + target.position;
            TargetDistance = Vector3.Distance(target.position, transform.position) - (targetInteractable.radius + agent.radius);
        }
        else
        {
            Destination = directionVector * (playerFollowOffset + agent.radius - margin) + target.position;
            TargetDistance = Vector3.Distance(target.position, transform.position) - agent.radius;
        }
        if (TryGetComponent<Animator>(out animator))
        {
            animator.SetFloat("speed", agent.velocity.magnitude);
            animator.SetFloat("speed multiplier", animationMultiplier);
        }
    }

    public void SetCombatStatus(Transform target)
    {
        IsInCombat = true;
        this.target = target;
    }

    public void ResetCombatStatus()
    {
        IsInCombat = false;
        target = player;
    }

    public void ResetPosition()
    {
        agent.Warp(player.GetComponent<AvatarActions>().spawnPoint.position + Vector3.back * playerFollowOffset);
    }

    private void OnDrawGizmos()
    {
        if (agent != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, agent.desiredVelocity + transform.position);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Destination, 1);
        }
    }
}
