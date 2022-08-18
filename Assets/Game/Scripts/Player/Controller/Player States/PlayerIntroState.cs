using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerIntroState : PlayerBaseState
{
    public UnityEvent Intro;
    public InputReader reader;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        reader.EnableIntroStageInput();
        reader.SkipIntroEvent += EndIntro;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EndIntro();
        reader.EnableGameplayInput();
        reader.SkipIntroEvent -= EndIntro;
    }

    private void EndIntro()
    {
        Intro.Invoke();
        fsm.ChangeState(fsm.IdleStateName);
    }
}
