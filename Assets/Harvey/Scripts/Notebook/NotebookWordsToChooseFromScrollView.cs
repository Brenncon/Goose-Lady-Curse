using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class NotebookWordsToChooseFromScrollView : MonoBehaviour
{
    [SerializeField] NotebookManager notebookManager;

    [SerializeField] Transform WordsBlockTransformPrefeb;

    RectTransform content;

    void Awake()
    {
        // Initialize
        content = GetComponent<ScrollRect>().content;

        // If there is no child element for the content, aka the game just started
        if (content.childCount == 0)
        {
            // For every emoji stored in the words list in the notebook manager
            for (int i = 0; i < notebookManager.words.Length; i++)
            {
                // Instantiate a draggable gameObject for each word
                NotebookWordBlock wordBlock = Instantiate(WordsBlockTransformPrefeb, content).GetComponent<NotebookWordBlock>();

                // Assign the canvas for the word suggestion block instantiated
                wordBlock.canvas = notebookManager.canvas;

                // Cache the text component
                TMP_Text wordText = wordBlock.transform.GetChild(0).GetComponent<TMP_Text>();

                // Assign the text to the instantiated gameObject
                wordText.text = notebookManager.words[i];
            }
        }
    }
}
