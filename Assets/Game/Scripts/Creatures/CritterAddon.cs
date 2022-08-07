using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polyperfect.Animals;
using Polyperfect.Common;
using UnityEngine.AI;

[RequireComponent(typeof(Animal_WanderScript))]
[RequireComponent(typeof(Interactable))]
public class CritterAddon : MonoBehaviour
{
    private Vector3 target;

    private bool hasAgent;
    private Transform player;
    private NavMeshAgent agent;
    private Animal_WanderScript animalAI;
    public CreatureHealthBar healthBar;
    public GameObject dropItem;
    public float defence;
    public float fleeDistance = 10f;
    public float fleeSpeed = 5f;
    public bool isDead;
    private enum State
    {
        flee,
        wander,
        death
    }
    [SerializeField, ReadOnly]
    private State currentState;

    private void Awake()
    {
        animalAI = GetComponent<Animal_WanderScript>();
        hasAgent = TryGetComponent<NavMeshAgent>(out agent);
        gameObject.tag = "Attackable";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Start()
    {
        currentState = State.wander;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar.SetMaxValue(animalAI.stats.toughness);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.flee:
                FleeStateUpdate();
                break;
            case State.wander:
                WanderStateUpdate();
                break;
        }
    }

    private void SetFleeState()
    {
        healthBar.gameObject.SetActive(true);
        currentState = State.flee;
        animalAI.ClearAnimatorBools();
        animalAI.TrySetBool("isRunning", true);
        animalAI.enabled = false;
    }

    private void SetWanderState()
    {
        animalAI.ClearAnimatorBools();
        healthBar.gameObject.SetActive(false);
        currentState = State.wander;
        animalAI.enabled = true;
    }

    private void SetDeathState()
    {
        animalAI.ClearAnimatorBools();
        healthBar.gameObject.SetActive(false);
        currentState = State.death;
        isDead = true;
        animalAI.enabled = false;
        animalAI.SetState(Common_WanderScript.WanderState.Dead);
        if (hasAgent)
            agent.enabled = false;
        if (dropItem != null)
        {
            GameObject.Instantiate(dropItem, transform.position, transform.rotation);
        }
    }

    private void FleeStateUpdate()
    {
        if ((transform.position - player.position).magnitude >= animalAI.awareness)
        {
            SetWanderState();
            return;
        }

        SetFleeDestination();
    }

    private void WanderStateUpdate()
    {
        if ((transform.position - player.position).magnitude < animalAI.awareness)
        {
            SetFleeState();
        }
    }

    private bool IsInDetectionRange()
    {
        if (Vector3.Distance(player.position, transform.position) <= animalAI.scent)
        {
            return true;
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = 0;
        actualDamage = Mathf.Clamp(damage, 0, damage - defence);
        animalAI.toughness -= actualDamage;
        healthBar.SetValue(animalAI.toughness);
        if (animalAI.toughness <= 0 && currentState != State.death)
        {
            SetDeathState();
        }
    }


    private void SetFleeDestination()
    {
        agent.speed = fleeSpeed;
        float distanceFromBoundary = animalAI.wanderZone - (transform.position - animalAI.origin).magnitude;
        //if close to boundary target point behind player
        if (distanceFromBoundary > animalAI.wanderZone)
        {
            agent.SetDestination(animalAI.origin);
        }
        else
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            target = transform.position + fleeDirection * fleeDistance;
            agent.SetDestination(transform.position + fleeDirection * fleeDistance);
        }
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.DrawWireSphere(target, 1);
        }
    }
}
