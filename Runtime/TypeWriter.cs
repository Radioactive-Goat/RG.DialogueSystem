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
        #region Singleton Setup
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
        #endregion

        /// <summary>
        /// This event will be called after each letter is typed
        /// strign here is for "newTextValue"
        /// </summary>
        public event Action<string> OnTextUpdated;
        /// <summary>
        /// This event will be called when the type writer starts typing
        /// </summary>
        public event Action OnStartTyping;
        /// <summary>
        /// This event will be called when the type writer finshed tying the string
        /// </summary>
        public event Action OnTypingComplete;
        /// <summary>
        /// Status of the type writer
        /// </summary>
        public bool IsTyping { get; private set; }

        [SerializeField] [Range(0.01f, 1f)]
        private float _timeGapBetweenLetters = 0.05f;
        private float TimeGapBetweenLetters
        {
            get
            {
                return _timeGapBetweenLetters;
            }
            set
            {
                _timeGapBetweenLetters = value;
                _timeGapInMs = (int)(TimeGapBetweenLetters * 1000);
            }
        }

        private float _defaultTypingSpeed;
        private int _timeGapInMs;
        private string _textToTypeOut, _currentlyTypedOutText;
        private Task _typeWriteEffectTask;
        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            _defaultTypingSpeed = TimeGapBetweenLetters;
            _timeGapInMs = (int)(TimeGapBetweenLetters * 1000);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        /// <summary>
        /// Starts the type writer to type out the required text
        /// </summary>
        /// <param name="textToType">The text that will be typed</param>
        public void StartTypeWriter(string textToType)
        {
            StopTypeWriter();

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Token.Register(() => _typeWriteEffectTask = null);
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
                    return;
                }
                _currentlyTypedOutText += _textToTypeOut[index].ToString();
                OnTextUpdated?.Invoke(_currentlyTypedOutText);
                index++;
                await Task.Delay(_timeGapInMs);
            }
            IsTyping = false;
            OnTypingComplete?.Invoke();
        }

        /// <summary>
        /// Completes typing the in progress string 
        /// </summary>
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

        /// <summary>
        /// Force Stops the Type Writer
        /// </summary>
        public void StopTypeWriter()
        {
            if (_typeWriteEffectTask != null)
            {
                _cancellationTokenSource.Cancel();
                IsTyping = false;
            }
        }

        /// <summary>
        /// Updates the default time gap between typing each letter
        /// </summary>
        /// <param name="newTimeGap">new time gap</param>
        public void UpdateTimeGapBetweenLetters(float newTimeGap)
        {
            TimeGapBetweenLetters = newTimeGap;
            _defaultTypingSpeed = newTimeGap;
        }

        /// <summary>
        /// Speeds up the type writer by setting the gap between typing each letter
        /// to provided value
        /// </summary>
        /// <param name="newtimeGapBetweenLetters">new time gap between typing each letter</param>
        public void SpeedUpWithSetValue(float newtimeGapBetweenLetters)
        {
            TimeGapBetweenLetters = newtimeGapBetweenLetters;
        }

        /// <summary>
        /// Speeds up the type writer by x amount
        /// </summary>
        /// <param name="multiplierValue">How much faster/slower should the typing be</param>
        public void SpeedUpWithMultiplier(float multiplierValue)
        {
            TimeGapBetweenLetters /= multiplierValue;
        }

        /// <summary>
        /// Returns to default time gap between typing each letter
        /// </summary>
        public void ReturnToDefaultSpeed()
        {
            TimeGapBetweenLetters = _defaultTypingSpeed;
        }
    }
}
