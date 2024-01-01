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
        public Action OnCharecterDisplay, OnCharacterHide;

        public void DisplayCharacter()
        {
            foreach (CharactorIconHandler charactorIcon in _charactersWithOverlapPossibility)
            {
                if(charactorIcon.IsActive)
                {
                    charactorIcon.HideCharacter();
                }
            }
            _characterToDisplay.SetActive(true);
            OnCharecterDisplay?.Invoke();
        }

        public void HideCharacter(bool invokeHideEvent = true)
        {
            _characterToDisplay.SetActive(false);
            if(invokeHideEvent)
            {
                OnCharacterHide?.Invoke();
            }
        }
    }
}
