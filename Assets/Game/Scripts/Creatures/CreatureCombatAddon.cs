using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polyperfect.Animals;
using Polyperfect.Common;
using UnityEngine.AI;

[RequireComponent(typeof(Animal_WanderScript))]
public class CreatureCombatAddon : MonoBehaviour
{
    [SerializeField]
    private float defence;
    private float playerRadius;
    private float safetyMargin = 1f;//navmesh agent always stop at 1f distance for some reason, need to count that in.
    private bool isAttacking;
    private bool hasAgent;
    private Vector3 targetDirection;
    private Transform player;
    private AvatarCombat combat;
    private NavMeshAgent agent;
    private Animal_WanderScript animalAI;
    private MovementState runningState;
    private CapsuleCollider creatureCollider;
    public bool isDead;
    private enum State
    {
        chase,
        attack,
        wander,
        death
    }
    [SerializeField, ReadOnly]
    private State currentState;

    private void Awake()
    {
        animalAI = GetComponent<Animal_WanderScript>();
        hasAgent = TryGetComponent<NavMeshAgent>(out agent);
        creatureCollider = GetComponent<CapsuleCollider>();
        foreach (MovementState state in animalAI.movementStates)
        {
            if (state.stateName == "Running")
            {
                runningState = state;
                break;
            }
        }
    }

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRadius = player.GetComponent<NavMeshAgent>().radius;
        combat = player.GetComponent<AvatarCombat>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.attack:
                AttackStateUpdate();
                break;
            case State.chase:
                ChaseStateUpdate();
                break;
            case State.wander:
                WanderStateUpdate();
                break;
        }

    }

    private void SetAttackState()
    {
        currentState = State.attack;
        animalAI.SetState(Common_WanderScript.WanderState.Attack);
        animalAI.enabled = false;
    }

    private void SetChaseState()
    {
        currentState = State.chase;
        animalAI.enabled = false;
    }

    private void SetWanderState()
    {
        currentState = State.wander;
        animalAI.enabled = true;
    }

    private void SetDeathState()
    {
        currentState = State.death;
        isDead = true;
        animalAI.enabled = false;
        animalAI.SetState(Common_WanderScript.WanderState.Dead);
        creatureCollider.enabled = false;
        if (hasAgent)
            agent.enabled = false;
    }

    private void AttackStateUpdate()
    {
        if (!IsInAttackRange() && !isAttacking)
        {
            SetChaseState();
            return;
        }
        targetDirection = (player.position - transform.position).normalized;
        animalAI.FaceDirection(targetDirection);
    }

    private void ChaseStateUpdate()
    {
        if (IsInAttackRange())
        {
            SetAttackState();
            return;
        }
        if (!IsInDetectionRange() || !animalAI.IsValidLocation(player.position))
        {
            SetWanderState();
            return;
        }
        targetDirection = (player.position - transform.position).normalized;
        animalAI.FaceDirection(targetDirection);
        PursuePlayer();
    }

    private void WanderStateUpdate()
    {
        if (IsInDetectionRange() && animalAI.IsValidLocation(player.position))
        {
            SetChaseState();
            return;
        }
    }

    private bool IsInAttackRange()
    {
        //Debug.Log(Vector3.Distance(player.position, transform.position));
        if (Vector3.Distance(player.position, transform.position) <= animalAI.attackReach + safetyMargin)
        {
            return true;
        }
        return false;
    }

    private bool IsInDetectionRange()
    {
        if (Vector3.Distance(player.position, transform.position) <= animalAI.scent)
        {
            return true;
        }
        return false;
    }

    private void PursuePlayer()
    {
        if (hasAgent)
        {
            Vector3 destination = player.position - targetDirection * (animalAI.attackReach);
            agent.SetDestination(destination);

            animalAI.SetState(Common_WanderScript.WanderState.Chase);
            agent.speed = runningState.moveSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = 0;
        actualDamage = Mathf.Clamp(damage, 0, damage - defence);
        animalAI.toughness -= actualDamage;
        if (animalAI.toughness <= 0 && currentState != State.death)
        {
            SetDeathState();
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void DealDamage()
    {
        combat.TakeDamage(animalAI.stats.power);
    }

    public void AttackComplete()
    {
        isAttacking = false;
    }
}
