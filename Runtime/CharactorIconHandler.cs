using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    public class CharactorIconHandler : MonoBehaviour
    {
        [SerializeField] GameObject _characterToDisplay;
        [SerializeField] CharactorIconHandler[] _charactersWithOverlapPossibility;
        public bool IsActive => _characterToDisplay.activeSelf;
        public bool IsActiveSpeaker { get; private set; }
        public Action OnCharecterDisplay, OnCharacterHide;
        public Action OnActiveSpeaker, OnActiveListner;


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

        public void HideCharacter(bool invokeHideEvent = true)
        {
            _characterToDisplay.SetActive(false);
            IsActiveSpeaker = false;
            if(invokeHideEvent)
            {
                OnCharacterHide?.Invoke();
            }
        }

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
