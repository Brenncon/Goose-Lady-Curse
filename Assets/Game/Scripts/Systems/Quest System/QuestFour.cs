using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFour : MonoBehaviour
{
    public GameObject catalyst;
    public List<QuestFourTrigger> triggers;
    private int currentTrigger;

    // Start is called before the first frame update
    void Start()
    {
        currentTrigger = 0;
        triggers[currentTrigger].isCorrectTrigger = true;
    }


    public void SetNextTrigger()
    {
        currentTrigger++;
        if (currentTrigger < triggers.Count)
        {
            triggers[currentTrigger].isCorrectTrigger = true;
        }
        else
        {
            GameObject.Instantiate(catalyst);
        }
        
    }
}
