using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    /// <summary>
    /// Handles the displaying of the charactor icon
    /// </summary>
    public class CharactorIconHandler : MonoBehaviour
    {
        [SerializeField] GameObject _characterToDisplay;
        [SerializeField] CharactorIconHandler[] _charactersWithOverlapPossibility;
        public bool IsActive => _characterToDisplay.activeSelf;
        public bool IsActiveSpeaker { get; private set; }
        public Action OnCharecterDisplay, OnCharacterHide;
        public Action OnActiveSpeaker, OnActiveListner;

        /// <summary>
        /// Displays the required character on screen
        /// </summary>
        public void DisplayCharacter()
        {
            foreach (CharactorIconHandler charactorIcon in _charactersWithOverlapPossibility)
            {
                if(charactorIcon.IsActive)
                {
                    charactorIcon.HideCharacter();
                }
            }
            SetAsActiveSpeaker();
            _characterToDisplay.SetActive(true);
            OnCharecterDisplay?.Invoke();
        }

        /// <summary>
        /// Hides the character on screen
        /// </summary>
        /// <param name="invokeHideEvent">If the on hide event should be invoked</param>
        public void HideCharacter(bool invokeHideEvent = true)
        {
            _characterToDisplay.SetActive(false);
            IsActiveSpeaker = false;
            if(invokeHideEvent)
            {
                OnCharacterHide?.Invoke();
            }
        }

        /// <summary>
        /// If character is being displayed but is not activly speaking
        /// then invokes the active listner event and adjustes values accordingly
        /// </summary>
        public void SetAsActiveListner()
        {
            if(IsActiveSpeaker)
            {
                OnActiveListner?.Invoke();
                IsActiveSpeaker = false;
            }
        }

        private void SetAsActiveSpeaker()
        {
            if(!IsActiveSpeaker)
            {
                OnActiveSpeaker?.Invoke();
                IsActiveSpeaker = true;
            }
        }
    }
}
