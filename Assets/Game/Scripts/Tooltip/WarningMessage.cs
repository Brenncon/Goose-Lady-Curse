using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text), typeof(Animation))]
public class WarningMessage : MonoBehaviour
{
    TMP_Text myText;
    Animation myAnimation;

    void Start()
    {
        // Initialize
        myText = GetComponent<TMP_Text>();
        myAnimation = GetComponent<Animation>();
    }

    // Function called when a warning message is needed
    public void StartAnimation(string warningMessageText)
    {
        // Set the warning message text to display
        myText.text = warningMessageText;

        // Play the fade out animation
        myAnimation.Stop();
        myAnimation.Play();
    }

    // Function called when the animation is finished
    public void FinishAnimation()
    {
        // Reset myText to null to prevent unexpected errors
        myText.text = null;
    }
}
