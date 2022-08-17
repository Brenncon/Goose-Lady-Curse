using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TooltipUI : MonoBehaviour
{
    RectTransform myRectTransform, canvasRectTransform;
    [SerializeField] RectTransform tooltipUIRectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_Text mytext;
    [SerializeField] Vector2 padding;

    void Start()
    {
        // Initialize
        myRectTransform = GetComponent<RectTransform>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        // Update tooltip position
        UpdateTooltipPosition();
    }

    public void SetText(string tooltipText)
    {
        // Set the text of the tooltip text
        mytext.text = tooltipText;

        // Make sure that it is updated
        mytext.ForceMeshUpdate();

        // Get the text size of the tooltip text
        Vector2 textSize = mytext.GetRenderedValues(false);

        // Resize the background to match the size of the tooltip text
        myRectTransform.sizeDelta = textSize+2*padding;
    }

    public void UpdateTooltipPosition()
    {
        // Move with the cursor
        Vector2 anchoredPosition = Mouse.current.position.ReadValue() / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + myRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            // Tooltip left screen on right side
            anchoredPosition.x = canvasRectTransform.rect.width - myRectTransform.rect.width;
        }
        if (anchoredPosition.y + myRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            // Tooltip left screen on right side
            anchoredPosition.y = canvasRectTransform.rect.height - myRectTransform.rect.height;
        }

        // Set the tooltip textbox to whereever the mouse is at
        tooltipUIRectTransform.anchoredPosition = anchoredPosition;
    }
}
