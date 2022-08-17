using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentValue : MonoBehaviour
{
    public SurvivalStat targetStat;
    public float value;
    public void SetValue()
    {
        targetStat.currentValue = value;
    }
}
