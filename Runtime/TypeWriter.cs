using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    public class TypeWriter : MonoBehaviour
    {
        [SerializeField] private float _timeGapBetweenLetters = 0.05f;
        public Action<string/*newTextValue*/> OnTextUpdated;

        private float _defaultTypingSpeed;
        private string _textToTypeOut, _currentlyTypedOutText;
        public bool IsTyping { get; private set; }

        private void Start()
        {
            _defaultTypingSpeed = _timeGapBetweenLetters;
        }

        public void StartTypeWriter(string textToType)
        {
            _textToTypeOut = textToType;
            _currentlyTypedOutText = "";
            //Start the Type Writer

        }

        public void StopTypeWriter()
        {
            //Stop Type Writer here
            IsTyping = false;
        }

        public void SkipToEnd()
        {
            _currentlyTypedOutText = _textToTypeOut;
            StopTypeWriter();
            OnTextUpdated?.Invoke(_currentlyTypedOutText);
        }

        public void UpdateTimeGapBetweenLetters(float newTimeGap)
        {
            _timeGapBetweenLetters = newTimeGap;
            _defaultTypingSpeed = newTimeGap;
        }

        public void SpeedUpWithSetValue(float newtimeGapBetweenLetters)
        {
            _timeGapBetweenLetters = newtimeGapBetweenLetters;
        }

        public void SpeedUpWithMultiplier(float multiplierValue)
        {
            _timeGapBetweenLetters /= multiplierValue;
        }

        public void ReturnToDefaultSpeed()
        {
            _timeGapBetweenLetters = _defaultTypingSpeed;
        }
    }
}
