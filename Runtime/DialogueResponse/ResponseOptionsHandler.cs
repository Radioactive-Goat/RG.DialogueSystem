using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RG.DialogueSystem
{
    public class ResponseOptionsHandler : MonoBehaviour
    {
        #region Singleton Setup
        public static ResponseOptionsHandler Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        #endregion

        [SerializeField] GameObject _responsesUiObject;
        [SerializeField] UiResponseOption[] _uiResponseOptions;
        [SerializeField] bool _shouldLoopNavigation;
        public event Action OnStartResponse;
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

        public void ShowResponseOptions(DialogResponseCollection responseCollection)
        {
            if(responseCollection.Responses.Length > _uiResponseOptions.Length)
            {
                Debug.LogError($"The responses in the collection; {responseCollection.name}, are greater than available UI slots");
                return;
            }

            ResetResponsesUI();
            _activeResponseCollection = responseCollection;
            CharactorDisplayHandler.Instance.DisplayCharactor(_activeResponseCollection.RespondingCharactor);
            for (int i = 0; i < responseCollection.Responses.Length; i++)
            {
                _uiResponseOptions[i].SetResponseText(responseCollection.Responses[i].Response);
            }
            HighlightResponseOption(0);
            OnStartResponse?.Invoke();
            _responsesUiObject.SetActive(true);
        }

        public void SelectResponse()
        {
            _uiResponseOptions[_currentHighlightedIndex].ConfirmOption();
            _responsesUiObject.SetActive(false);
            OnEndResponse?.Invoke();
            if (_activeResponseCollection.Responses[_currentHighlightedIndex].FollowUpDialogues != null)
            {
                DialogFlowHandler.Instance.StartNewDialogChain(_activeResponseCollection.Responses[_currentHighlightedIndex].FollowUpDialogues);
            }
            else
            {
                DialogFlowHandler.Instance.InformCollectionEded();
            }
        }

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
