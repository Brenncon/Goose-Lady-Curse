using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarRootMotion : AvatarLocomotion
{
    //[Tooltip("position Change Rate when path is ending")]
    //public float positionDampeningTime = 5.0f;
    //[Tooltip("position Interpolation Threshold")]
    //public float positionDeadZone = 0.001f;
    [Tooltip("Speed Change Rate")]
    public float speedDampeningTime = 5.0f;
    [Tooltip("Speed Interpolation Threshold")]
    public float speedDeadZone = 0.001f;
    [Tooltip("Changes the avatar max speed ranges from 1 to 5"),Range(1,5)]
    public float speedMultiplier = 1.0f;
    [Tooltip("Avatar will slowdown within this distance")]
    public float brakingDistance = 5.0f;
    [Tooltip("Scale the braking strength with speed multiplier")]
    public float brakingCoefficient = 0.5f;
    [Tooltip("Scale the turning speed with speed multiplier")]
    public float turningCoefficient = 1.2f;
    [Tooltip("Angular damping time at minimal speed multiplier")]
    public float baseAngularDampingTime = 4.0f;
    private readonly int SpeedParameter = Animator.StringToHash("speed");
    private readonly int SpeedMultiplierParameter = Animator.StringToHash("speed multiplier");
    // speed control variable
    private float targetDriveSignal;
    // speed actuating variable
    private float currentDriveSignal;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Disable navmesh agent's steering functionality 
        agent.speed = 1.1f;
        agent.angularSpeed = 0;
        agent.acceleration = 0;

        if (hasAnimator)
        {
            avatarAnimator.SetFloat(SpeedMultiplierParameter, speedMultiplier);
        }
    }

    private void OnAnimatorMove()
    {
        //Root motion
        agent.velocity = avatarAnimator.deltaPosition / Time.deltaTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (hasAnimator)
        {
#if UNITY_EDITOR
            avatarAnimator.SetFloat(SpeedMultiplierParameter, speedMultiplier);
#endif
            //agent.stoppingDistance = stoppingDistanceCoefficient * speedMultiplier;
            #region Navigation Control
            //Stop, Brake, Cruise
            if (agent.remainingDistance < agent.stoppingDistance && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                targetDriveSignal = 0;
            }
            else if (agent.remainingDistance >= agent.stoppingDistance && agent.remainingDistance < brakingDistance)
            {
                targetDriveSignal = Mathf.Lerp(targetDriveSignal, 0, (agent.remainingDistance * brakingCoefficient * speedMultiplier / (brakingDistance * brakingDistance)) * Time.deltaTime);
            }
            else
            {
                targetDriveSignal = agent.desiredVelocity.magnitude;
            }
            #endregion

            #region Speed Control
            //Speed changes gradually
            if (Mathf.Abs(currentDriveSignal - targetDriveSignal) > speedDeadZone)
            {
                currentDriveSignal = Mathf.Lerp(currentDriveSignal, targetDriveSignal, speedDampeningTime * Time.deltaTime);
            }
            else
            {
                currentDriveSignal = targetDriveSignal;
            }
            avatarAnimator.SetFloat(SpeedParameter, currentDriveSignal);
            #endregion
            angularDampeningTime = baseAngularDampingTime + turningCoefficient * speedMultiplier;
        }

        base.Update();
    }


}
