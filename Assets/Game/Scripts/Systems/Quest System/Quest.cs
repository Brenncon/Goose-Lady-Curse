using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.Events;
using Project.Build.Commands;

[RequireComponent(typeof(Flowchart))]
public class Quest : MonoBehaviour
{
    private Flowchart dialog;
    [SerializeField,ReadOnly]
    private Objective currentObjective;
    private int objectiveIndex;
    public bool missionComplete;
    public List<Objective> objectives;
    public Quest nextQuest;
    public List<GameObject> emojiHint;
    public UnityEvent<List<GameObject>> SetEmojiEvent;
    public UnityEvent ClearEmojiEvent;
    public UnityEvent<string> QuestCompleteInstruction;
    public UnityEvent QuestOver;
    public enum Stage
    {
        start,
        ongoing,
        completed
    }
    public Stage stage;

    private void OnEnable()
    {
        //save system needs to assign the stage according to progression
        stage = Stage.start;
        dialog = GetComponent<Flowchart>();
        if (objectives.Count > 0)
        {
            currentObjective = objectives[0];
        }
    }

    public void Dialog()
    {
        switch (stage)
        {
            case Stage.start:
                dialog.SendFungusMessage("intro");
                if (objectives.Count > 0)
                {
                    OnQuestIncomplete();
                    currentObjective.SetObjective();
                    currentObjective.ObjectiveComplete.AddListener(ObjectiveStateChange);
                    SetEmojiEvent.Invoke(emojiHint);
                }
                else
                {
                    OnQuestComplete();
                }
                break;
            case Stage.ongoing:
                dialog.SendFungusMessage("ongoing");
                break;
            case Stage.completed:
                break;

        }
    }

    public virtual void SkipDialog()
    {
        dialog.StopAllBlocks();
        if (objectives.Count == 0)
        {
            stage = Stage.completed;
            QuestOver.Invoke();
            return;
        }
        //if (stage == Stage.ongoing)
        //{
        //    SetEmojiEvent.Invoke(emojiHint);
        //}
    }

    public void OnQuestComplete()
    {
        stage = Stage.completed;
        missionComplete = true;
        ClearEmojiEvent.Invoke();
        QuestCompleteInstruction.Invoke("");
    }

    public void OnQuestIncomplete()
    {
        stage = Stage.ongoing;
        missionComplete = false;
    }

    public void SetEmoji()
    {
        SetEmojiEvent.Invoke(emojiHint);
    }

    public void ObjectiveStateChange(bool state)
    {
        if (state)
        {
            currentObjective.ObjectiveComplete.RemoveListener(ObjectiveStateChange);
            objectiveIndex++;
            
            if (objectiveIndex>=objectives.Count)
            {
                OnQuestComplete();
            }
            else
            {
                currentObjective = objectives[objectiveIndex];
                currentObjective.SetObjective();
                currentObjective.ObjectiveComplete.AddListener(ObjectiveStateChange);
            }
            
        }
        else
        {
            if (objectiveIndex - 1 >= 0)
            {
                currentObjective.ObjectiveComplete.RemoveListener(ObjectiveStateChange);
                currentObjective = objectives[objectiveIndex--];
                currentObjective.SetObjective();
            }
            currentObjective.ObjectiveComplete.AddListener(ObjectiveStateChange);
        }
    }
}
