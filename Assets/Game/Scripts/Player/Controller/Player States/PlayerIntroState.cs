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
        reader.DisableAllInput();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Intro.Invoke();
        reader.EnableGameplayInput();
        Debug.Log("intro ended");
    }
}
