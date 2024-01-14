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

        public UnityEvent OnHighlighted, OnHighlightRemoved, OnOptionConfiremed;

        public void SetResponseText(string response)
        {
            _responseText.SetText(response);
        }

        public void HighlightOption()
        {
            _highlightedIndicator?.SetActive(true);
            OnHighlighted?.Invoke();
        }

        public void RemoveHighlight()
        {
            _highlightedIndicator?.SetActive(false);
            OnHighlightRemoved?.Invoke();
        }

        public void ConfirmOption()
        {
            OnOptionConfiremed?.Invoke();
        }

        public void ResetUi()
        {
            _highlightedIndicator?.SetActive(false);
            _responseText.SetText(string.Empty);
        }
    }
}
