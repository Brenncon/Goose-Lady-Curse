using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
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
    public AudioClip hit;
    public AudioClip mine;


    private Animator avatarAnimator;
    private int attackAnimationHash = Animator.StringToHash("attack");
    private int attackSpeedHash = Animator.StringToHash("attack speed");
    public float animationLength = 1;
    private float animationSpeed;
    private AudioSource playerAudio;
    [SerializeField,ReadOnly]
    private Transform target;
    private void Start()
    {
        avatarAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
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
            playerAudio.PlayOneShot(hit);
            targetDead = creatureCombatAddon.isDead;
        }
        if (target != null && target.TryGetComponent<CritterAddon>(out CritterAddon critterAddon))
        {
            critterAddon.TakeDamage(attackDamage.finalValue);
            playerAudio.PlayOneShot(hit);
            targetDead = critterAddon.isDead;
        }
        //for the rock and minerals
        if (target != null && target.TryGetComponent<DestructableObject>(out DestructableObject destructableObject))
        {
           destructableObject.TakeDamage(attackDamage.finalValue);
            playerAudio.PlayOneShot(mine);
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
