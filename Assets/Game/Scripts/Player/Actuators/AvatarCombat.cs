using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AvatarCombat : MonoBehaviour
{
    [ReadOnly]
    public bool attacking = false;
    public Stat attackDamage;
    public Stat attackRange;
    public Stat attackInterval;
    public Stat defence;
    public SurvivalStat health;
    public bool targetDead;

    private Animator avatarAnimator;
    private int attackAnimationHash = Animator.StringToHash("attack");
    private int attackSpeedHash = Animator.StringToHash("attack speed");
    public float animationLength = 1;
    private float animationSpeed;
    [SerializeField,ReadOnly]
    private Transform target;
    private void Start()
    {
        avatarAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (attacking)
        {
            animationSpeed = animationLength / attackInterval.finalValue;
            avatarAnimator.SetFloat(attackSpeedHash, animationSpeed);
        }
    }

    public void DamageTarget()
    {
        
        if (target != null && target.TryGetComponent<CreatureCombatAddon>(out CreatureCombatAddon creatureCombatAddon))
        {
            creatureCombatAddon.TakeDamage(attackDamage.finalValue);
            targetDead = creatureCombatAddon.isDead;
        }

        Debug.Log(target);
        //for the rock and minerals
        if (target != null && target.TryGetComponent<DestructableObject>(out DestructableObject destructableObject))
        {
           destructableObject.TakeDamage(attackDamage.finalValue);
        }
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = 0;
        actualDamage = Mathf.Clamp(damage, 0, damage - defence.finalValue);
        health.currentValue -= actualDamage;
    }

    public bool TargetIsDead()
    {
        return targetDead;
    }

    public void Attack(Transform target)
    {
        attacking = true;
        this.target = target;
        avatarAnimator.SetBool(attackAnimationHash, attacking);
    }

    public void CancelAttack()
    {
        attacking = false;
        target = null;
        avatarAnimator.SetBool(attackAnimationHash,attacking);
    }
}
