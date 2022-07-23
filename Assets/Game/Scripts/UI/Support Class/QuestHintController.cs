using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestHintController : MonoBehaviour
{
    public TMP_Text questHint;
    public void SetQuestHint(string hint)
    {
        questHint.text = hint;
    }
}
