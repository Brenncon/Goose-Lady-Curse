using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClickHintController : MonoBehaviour
{
    public RectTransform hint;
    public Vector2 offset = new Vector2(0,5);
    [SerializeField] RectTransform canvasRectTransform;

    public void ShowHint(Vector2 position)
    {
        position /= canvasRectTransform.localScale.x;

        hint.anchoredPosition = position+offset;
        hint.gameObject.SetActive(true);
    }

    public void HideHint()
    {
        hint.gameObject.SetActive(false);
    }
}
