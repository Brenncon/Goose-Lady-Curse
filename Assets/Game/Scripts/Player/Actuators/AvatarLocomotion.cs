using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AvatarLocomotion : MonoBehaviour
{
    public float speedDampeningTime = 3;
    public float speedDeadZone = 0.001f;
    public float angularDampeningTime = 5.0f;
    public float angularDeadZone = 10.0f;
    public float speedMultiplier = 1;
    public float brakingDistance = 5;

#if UNITY_EDITOR
    public float agentSpeed;
#endif

    private readonly int SpeedParameter = Animator.StringToHash("speed");
    private readonly int SpeedMultiplierParameter = Animator.StringToHash("speed multiplier");
    private AnimationListener animationListener;
    private NavMeshAgent agent;
    private Animator avatarAnimator;
    private float targetDriveSignal;
    private float currentDriveSignal;
    private bool hasAnimationListner;
    private bool hasAnimator;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        hasAnimationListner = TryGetComponent<AnimationListener>(out animationListener);
        hasAnimator = TryGetComponent<Animator>(out avatarAnimator);
    }
    // Start is called before the first frame update
    void Start()
    {
        //agent.speed = 1;
        agent.angularSpeed = 0;

        if (hasAnimationListner)
        {
            animationListener.OnAnimatorMoveEvent += OnAnimatorMove;
        }
        if (hasAnimator)
        {
            avatarAnimator.SetFloat(SpeedMultiplierParameter, speedMultiplier);
        }
    }

    private void OnAnimatorMove()
    {
        agent.velocity = avatarAnimator.deltaPosition / Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        agentSpeed = agent.velocity.magnitude;
        if (hasAnimator)
        {
            avatarAnimator.SetFloat(SpeedMultiplierParameter, speedMultiplier);
        }
#endif
        if (hasAnimator)
        {
            if (agent.remainingDistance < agent.stoppingDistance && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {

                targetDriveSignal = 0;
                //agent.velocity = Vector3.zero;
            }
            else if (agent.remainingDistance>=agent.stoppingDistance&&agent.remainingDistance<brakingDistance)
            {
                //targetSpeed -= agent.remainingDistance*Time.deltaTime / (2f * brakingDistance * avatarAnimator.deltaPosition.magnitude * avatarAnimator.deltaPosition.magnitude);
                targetDriveSignal = Mathf.Lerp(targetDriveSignal, 0, (agent.remainingDistance*0.5f*speedMultiplier/(brakingDistance*brakingDistance))*Time.deltaTime);
            }
            else
            {
                targetDriveSignal = agent.desiredVelocity.magnitude;
            }

            if (Mathf.Abs(currentDriveSignal - targetDriveSignal) > speedDeadZone)
            {
                currentDriveSignal = Mathf.Lerp(currentDriveSignal, targetDriveSignal, speedDampeningTime * Time.deltaTime);
            }
            else
            {
                currentDriveSignal = targetDriveSignal;
            }
            avatarAnimator.SetFloat(SpeedParameter, currentDriveSignal);

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                float angle = Vector3.Angle(transform.forward, agent.desiredVelocity);
                if (Mathf.Abs(angle) <= angularDeadZone)
                {
                    transform.LookAt(transform.position + agent.desiredVelocity);

                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                                                         Quaternion.LookRotation(agent.desiredVelocity),
                                                         Time.deltaTime * angularDampeningTime);

                }

            }

        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
