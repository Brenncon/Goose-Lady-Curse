using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class QuestFourTrigger : Interactable
{
    public bool isCorrectTrigger;
    public bool isTriggered;
    public QuestFour quest;
    public SurvivalStat health;

    private void Awake()
    {
        gameObject.tag = "Item";
    }

    public void OnTriggerClicked()
    {
        if (isCorrectTrigger&&!isTriggered)
        {
            isTriggered = true;
            quest.SetNextTrigger();
        }
        else
        {
            health.currentValue -= 25;
        }
    }
}
