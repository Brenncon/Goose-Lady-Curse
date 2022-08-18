using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Objective", menuName = "Quest/Objectve")]
public class Objective : ScriptableObject
{
    [SerializeField, ReadOnly]
    protected bool completed;
    protected bool started;
    public bool inverseEventSignal;
    public string instruction;
    [HideInInspector]
    public UnityEvent<bool> ObjectiveComplete;
    public UnityEvent<string> SetInstruction;

    public virtual void Initialize()
    {
        completed = false;
        started = false;
    }

    public void SetObjective()
    {
        Debug.Log("instruction:" + instruction);
        SetInstruction.Invoke(instruction);
        if (completed)
        {
            OnCompletion(true);
        }
        else
        {
            started = true;
        }
    }

    public void OnCompletion(bool state)
    {
        if (!started || completed)
        {
            return;
        }
        if (inverseEventSignal)
        {
            state = !state;
        }
        //Debug.Log(name+" "+state);
        ObjectiveComplete.Invoke(state);
        completed = state;
    }
}
