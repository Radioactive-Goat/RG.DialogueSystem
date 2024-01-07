using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    public class DialogFlowHandler : MonoBehaviour
    {
        #region Singleton Setup
        public static DialogFlowHandler Instance;
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

        private DialogCollection _currentCollection;
        private int _dialogueIndex;
        public Action OnCollectionEnded;
        public Action OnForceEnded;

        public void StartNewDialogChain(DialogCollection collection)
        {
            if(collection.Dialogs.Length == 0)
            {
                Debug.LogError($"Collection Passed ({collection.name}) is invald, as the array of dialogs is empty");
                return;
            }
            SetDialogCollection(collection);
            StartDialogue();
        }

        private void StartDialogue()
        {
            CharactorDisplayHandler.Instance?.DisplayCharactor(_currentCollection.Dialogs[_dialogueIndex].Charactor);
            TypeWriter.Instance.StartTypeWriter(_currentCollection.Dialogs[_dialogueIndex].Dialogue);
        }

        public void SetDialogCollection(DialogCollection collection)
        {
            _currentCollection = collection;
            _dialogueIndex = 0;
        }

        public void NextDialogue()
        {
            if(!TypeWriter.Instance.IsTyping)
            {
                _dialogueIndex++;
                if(_dialogueIndex >= _currentCollection.Dialogs.Length)
                {
                    OnCollectionEnded?.Invoke();
                    return;
                }

                StartDialogue();
            }
        }

        public void ForceEndCollection()
        {
            TypeWriter.Instance.StopTypeWriter();
            OnForceEnded?.Invoke();
        }
    }
}
