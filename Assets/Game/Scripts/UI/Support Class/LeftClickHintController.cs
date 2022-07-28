using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftClickHintController : MonoBehaviour
{
    public RectTransform hint;
    public Vector2 offset = new Vector2(0, 5);
    public Vector2 localPosition;
    public RectTransform gameplayUI;

    public void ShowHint(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameplayUI,position,null,out localPosition);
        hint.anchoredPosition = localPosition + offset;
        hint.gameObject.SetActive(true);
    }

    public void HideHint()
    {
        hint.gameObject.SetActive(false);
    }
}
