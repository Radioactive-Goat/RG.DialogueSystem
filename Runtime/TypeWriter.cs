using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RG.DialogueSystem
{
    public class TypeWriter : MonoBehaviour
    {
        [SerializeField] [TextArea(2, 3)]
        private string testingStr;
        [SerializeField] [Range(0.01f, 1f)]
        private float _timeGapBetweenLetters = 0.05f;
        public Action<string/*newTextValue*/> OnTextUpdated;
        public bool IsTyping { get; private set; }

        private float _defaultTypingSpeed;
        private int _timeGapInMs;
        private string _textToTypeOut, _currentlyTypedOutText;
        //private Coroutine _typeWriteEffectCoroutine;
        private Task _typeWriteEffectTask;

        private void Start()
        {
            _defaultTypingSpeed = _timeGapBetweenLetters;
            _timeGapInMs = (int)(_timeGapBetweenLetters * 1000);
            StartTypeWriter(testingStr);
        }

        private void OnDestroy()
        {
            //_typeWriteEffectTask.Dispose();
        }

        public void StartTypeWriter(string textToType)
        {
            StopTypeWriter();

            _textToTypeOut = textToType;
            _currentlyTypedOutText = "";
            _typeWriteEffectTask = TypeWriteEffect();
        }

        public void StopTypeWriter()
        {
            if (_typeWriteEffectTask != null )
            {
                //_typeWriteEffectTask.Dispose();
                IsTyping = false;
            }
        }

        public void SkipToEnd()
        {
            _currentlyTypedOutText = _textToTypeOut;
            StopTypeWriter();
            OnTextUpdated?.Invoke(_currentlyTypedOutText);
        }

        private async Task TypeWriteEffect()
        {
            _currentlyTypedOutText = "";
            IsTyping = true;
            int index = 0;
            while (index < _textToTypeOut.Length)
            {
                _currentlyTypedOutText += _textToTypeOut[index].ToString();
                index++;
                Debug.Log(_currentlyTypedOutText);
                await Task.Delay(_timeGapInMs);
            }
            IsTyping = false;
        }

        public void UpdateTimeGapBetweenLetters(float newTimeGap)
        {
            _timeGapBetweenLetters = newTimeGap;
            _timeGapInMs = (int)(_timeGapBetweenLetters * 1000);
            _defaultTypingSpeed = newTimeGap;
        }

        public void SpeedUpWithSetValue(float newtimeGapBetweenLetters)
        {
            _timeGapBetweenLetters = newtimeGapBetweenLetters;
            _timeGapInMs = (int)(_timeGapBetweenLetters * 1000);
        }

        public void SpeedUpWithMultiplier(float multiplierValue)
        {
            _timeGapBetweenLetters /= multiplierValue;
            _timeGapInMs = (int)(_timeGapBetweenLetters * 1000);
        }

        public void ReturnToDefaultSpeed()
        {
            _timeGapBetweenLetters = _defaultTypingSpeed;
        }
    }
}
