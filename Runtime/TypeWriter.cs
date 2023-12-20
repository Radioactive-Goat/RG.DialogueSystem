using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace RG.DialogueSystem
{
    public class TypeWriter : MonoBehaviour
    {
        public static TypeWriter Instance;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        [SerializeField] [Range(0.01f, 1f)]
        private float _timeGapBetweenLetters = 0.05f;
        public Action<string/*newTextValue*/> OnTextUpdated;
        public Action OnStartTyping, OnTypingComplete;
        public bool IsTyping { get; private set; }

        private float _defaultTypingSpeed;
        private int _timeGapInMs;
        private string _textToTypeOut, _currentlyTypedOutText;
        private Task _typeWriteEffectTask;
        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            _defaultTypingSpeed = _timeGapBetweenLetters;
            _timeGapInMs = (int)(_timeGapBetweenLetters * 1000);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }

        public void StartTypeWriter(string textToType)
        {
            StopTypeWriter();

            _cancellationTokenSource = new CancellationTokenSource();
            _textToTypeOut = textToType;
            _currentlyTypedOutText = "";
            _typeWriteEffectTask = TypeWriteEffect(_cancellationTokenSource.Token);
            OnStartTyping?.Invoke();
        }

        private async Task TypeWriteEffect(CancellationToken cancellationToken)
        {
            _currentlyTypedOutText = "";
            IsTyping = true;
            int index = 0;
            while (index < _textToTypeOut.Length)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    goto endItPlease;
                }
                _currentlyTypedOutText += _textToTypeOut[index].ToString();
                OnTextUpdated?.Invoke(_currentlyTypedOutText);
                index++;
                await Task.Delay(_timeGapInMs);
            }
            IsTyping = false;
            OnTypingComplete?.Invoke();

        endItPlease:;
            _typeWriteEffectTask = null;
        }

        public void SkipToEnd()
        {
            if(IsTyping)
            {
                StopTypeWriter();
                _currentlyTypedOutText = _textToTypeOut;
                OnTextUpdated?.Invoke(_currentlyTypedOutText);
                OnTypingComplete?.Invoke();
            }
        }

        public void StopTypeWriter()
        {
            if (_typeWriteEffectTask != null)
            {
                _cancellationTokenSource.Cancel();
                IsTyping = false;
            }
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
            _timeGapInMs = (int)(_timeGapBetweenLetters * 1000);
        }
    }
}
