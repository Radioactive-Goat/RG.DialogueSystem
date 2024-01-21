using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RG.DialogueSystem
{
    public class UiResponseOption : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _responseText;
        [SerializeField] GameObject _highlightedIndicator;

        /// <summary>
        /// Events for specific states of the response option
        /// </summary>
        public UnityEvent OnHighlighted, OnHighlightRemoved, OnOptionConfiremed;

        /// <summary>
        /// Sets the display text of the responce optino
        /// </summary>
        /// <param name="response">The content of the response text</param>
        public void SetResponseText(string response)
        {
            _responseText.SetText(response);
        }

        /// <summary>
        /// Highlights this responce option
        /// </summary>
        public void HighlightOption()
        {
            _highlightedIndicator?.SetActive(true);
            OnHighlighted?.Invoke();
        }

        /// <summary>
        /// Removes the highlight from this response option
        /// </summary>
        public void RemoveHighlight()
        {
            _highlightedIndicator?.SetActive(false);
            OnHighlightRemoved?.Invoke();
        }

        /// <summary>
        /// Runs the the on confirm event
        /// </summary>
        public void ConfirmOption()
        {
            OnOptionConfiremed?.Invoke();
        }

        /// <summary>
        /// Resets the Ui by clearning the text field and removing highlight
        /// </summary>
        public void ResetUi()
        {
            _highlightedIndicator?.SetActive(false);
            _responseText.SetText(string.Empty);
        }
    }
}
