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

        private DialogResponseCollection activeResponseCollection;
        private int _maxPossibleResponses;
        private int _currentHighlightedIndex;

        private void Start()
        {
            _maxPossibleResponses = _uiResponseOptions.Length;
            _currentHighlightedIndex = 0;
        }

        public void ShowResponsOptions(DialogResponseCollection responses)
        {

        }

        public void SelectResponse()
        {

        }

        public void NavigateUp()
        {

        }

        public void NavigateDown()
        {

        }

        public void ResetResponcesUI()
        {
            foreach (UiResponseOption responseOption in _uiResponseOptions)
            {
                responseOption.ResetUi();
            }
        }
    }
}
