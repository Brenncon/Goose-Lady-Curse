using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// This class handles the behavior when the player issues the attack command
/// Under this state the avatar will chase and attack the target until:
///     1) the command is canceled
///     2) the enemy is dead
///     3) the avatar is dead
/// </summary>
public class PlayerAttackState : PlayerBaseState
{
    public UnityEvent<bool> EnterCombat;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneLoader.Instance.OnSceneUnloadedEvent += UnRegisterCallbacks;
        EnterCombat.Invoke(true);
        //fsm.inputReader.EnableGameplayInput();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fsm.motion.target == null)
        {
            IdleState();
        }
        if (fsm.actions.IsInAttackRange())
        {
            fsm.motion.FaceTarget();
            fsm.motion.Halt();
            fsm.actions.Attack(fsm.motion.target);
        }
        else
        {
            fsm.actions.CancelAttack();
            fsm.motion.MoveToTarget(fsm.actions.attackRange);
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fsm.actions.CancelAttack();
        EnterCombat.Invoke(false);
        UnRegisterCallbacks();
    }
    /// <summary>
    /// This method unregisters all call back in this class, specially when scene is unloaded to wipe the slate clean.
    /// </summary>
    public void UnRegisterCallbacks()
    {
        SceneLoader.Instance.OnSceneUnloadedEvent -= UnRegisterCallbacks;
    }

    public void IdleState()
    {
        fsm.ChangeState(fsm.IdleStateName);
    }
}
