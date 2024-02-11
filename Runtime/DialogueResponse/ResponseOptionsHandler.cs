using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RG.DialogueSystem
{
    public class ResponseOptionsHandler : MonoBehaviour
    {
        [SerializeField] GameObject _responsesUiObject;
        [SerializeField] UiResponseOption[] _uiResponseOptions;
        [SerializeField] bool _shouldLoopNavigation;
        /// <summary>
        /// Even called when a response list if shown
        /// </summary>
        public event Action OnStartResponse;
        /// <summary>
        /// Event called when the response list is closed
        /// </summary>
        public event Action OnEndResponse;

        private DialogResponseCollection _activeResponseCollection;
        private int _maxPossibleResponses;
        private int _currentHighlightedIndex;

        private void Start()
        {
            _maxPossibleResponses = _uiResponseOptions.Length;
            _currentHighlightedIndex = -1;
            _responsesUiObject.SetActive(false);
        }

        /// <summary>
        /// Shows the list of available responses based on the 
        /// provided response collection
        /// </summary>
        /// <param name="responseCollection">The collection of responses to be displayed</param>
        public void ShowResponseOptions(DialogResponseCollection responseCollection)
        {
            if(responseCollection.Responses.Length > _uiResponseOptions.Length)
            {
                Debug.LogError($"The responses in the collection; {responseCollection.name}, are greater than available UI slots");
                return;
            }

            ResetResponsesUI();
            _activeResponseCollection = responseCollection;
            DialogueSystemRefs.Instance.CharactorDisplayHandler.DisplayCharactor(_activeResponseCollection.RespondingCharactor);
            for (int i = 0; i < responseCollection.Responses.Length; i++)
            {
                _uiResponseOptions[i].SetResponseText(responseCollection.Responses[i].Response);
            }
            HighlightResponseOption(0);
            OnStartResponse?.Invoke();
            _responsesUiObject.SetActive(true);
        }

        /// <summary>
        /// Selected the highlighted response as the useres choice
        /// </summary>
        public void SelectResponse()
        {
            _uiResponseOptions[_currentHighlightedIndex].ConfirmOption();
            _responsesUiObject.SetActive(false);
            OnEndResponse?.Invoke();
            DialogResponseData responseData = _activeResponseCollection.Responses[_currentHighlightedIndex];
            if (responseData.FollowUpDialogues != null)
            {
                if (responseData.DialogResponseEvent != null)
                {
                    responseData.DialogResponseEvent.InvokeEvent();
                }
                DialogueSystemRefs.Instance.DialogFlowHandler.StartNewDialogChain(responseData.FollowUpDialogues);
            }
            else
            {
                if (responseData.DialogResponseEvent != null)
                {
                    responseData.DialogResponseEvent.InvokeEvent();
                }
                DialogueSystemRefs.Instance.DialogFlowHandler.InformCollectionEnded();
            }
        }

        /// <summary>
        /// Navigate through the list by one click down
        /// </summary>
        public void NavigateUp()
        {
            int newHighlight = _currentHighlightedIndex - 1;
            if(newHighlight < 0)
            {
                if(_shouldLoopNavigation)
                {
                    newHighlight = _activeResponseCollection.Responses.Length - 1;
                }
                else
                {
                    newHighlight = 0;
                }
            }
            HighlightResponseOption(newHighlight);
        }

        /// <summary>
        /// Navigate through the list by one click up
        /// </summary>
        public void NavigateDown()
        {
            int newHighlight = _currentHighlightedIndex + 1;
            if (newHighlight >= _activeResponseCollection.Responses.Length)
            {
                if (_shouldLoopNavigation)
                {
                    newHighlight = 0;
                }
                else
                {
                    newHighlight = _currentHighlightedIndex;
                }
            }
            HighlightResponseOption(newHighlight);
        }

        private void HighlightResponseOption(int index)
        {
            if(_currentHighlightedIndex != index)
            {
                if(_currentHighlightedIndex >= 0)
                {
                    _uiResponseOptions[_currentHighlightedIndex].RemoveHighlight();
                }
                _currentHighlightedIndex = index;
                _uiResponseOptions[_currentHighlightedIndex].HighlightOption();
            }
        }

        /// <summary>
        /// Resets the Ui of all the items on the response list
        /// </summary>
        public void ResetResponsesUI()
        {
            foreach (UiResponseOption responseOption in _uiResponseOptions)
            {
                responseOption.ResetUi();
            }
            _currentHighlightedIndex = -1;
        }
    }
}
