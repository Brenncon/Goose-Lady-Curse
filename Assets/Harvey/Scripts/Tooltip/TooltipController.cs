using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TooltipController : MonoBehaviour
{
    int UILayer;
    [SerializeField] TooltipUI tooltipUI;

    void Start()
    {
        // Initialize
        UILayer = LayerMask.NameToLayer("UI");
    }

    void Update()
    {
        // See if the mouse is over an UI element
        // Probably should have a function to handle all of these
        GameObject uiGameObject = IsPointerOverUIElement(GetEventSystemRaycastResults());

        // If there is an UI gameObject
        if (uiGameObject != null)
        {
            // If that UI gameObject is a menu button
            if (uiGameObject.GetComponent<MenuButtonClassifier>() != null)
            {
                // Throw a debug message
                Debug.Log(uiGameObject.GetComponent<MenuButtonClassifier>().tooltipText);

                // Ask tooltip UI to display tooltip text
                tooltipUI.SetText(uiGameObject.GetComponent<MenuButtonClassifier>().tooltipText);
                tooltipUI.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Not an UI");

                // Ask tooltip UI to hide the tooltip text
                tooltipUI.gameObject.SetActive(false);
            }
        }
    }

    // Method to get the reference to a UI element that the mouse is hovering
    private GameObject IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            // Cache the raycast result
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];

            // If that gameObject is on the UI layer
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                // Return the Ui gameObject
                return curRaysastResult.gameObject;
            }
        }
        // Return null, meaning the gameObject is not from the UI layer
        return null;
    }

    // Gets all event system raycast results of current mouse position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> raysastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, raysastResults);

        return raysastResults;
    }
}
