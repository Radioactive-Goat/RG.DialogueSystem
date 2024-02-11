using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    public class DialogFlowHandler : MonoBehaviour
    {
        private TypeWriter _typeWriter;
        private DialogCollection _currentCollection;
        private int _dialogueIndex;
        /// <summary>
        /// Event called when a new dialog chain starts
        /// </summary>
        public event Action OnDialogChainStart;
        /// <summary>
        /// Event called when the full collectino of dialogs and responses are completed
        /// </summary>
        public event Action OnCollectionEnded;
        /// <summary>
        /// Event called when the collection is forceablily ended
        /// </summary>
        public event Action OnForceEnded;

        private void Start()
        {
            _typeWriter = DialogueSystemRefs.Instance.TypeWriter;
        }

        /// <summary>
        /// Playes a new dialog chain
        /// </summary>
        /// <param name="collection">The collection of dialogs</param>
        public void StartNewDialogChain(DialogCollection collection)
        {
            if(collection.Dialogs.Length == 0)
            {
                Debug.LogError($"Collection Passed ({collection.name}) is invald, as the array of dialogs is empty");
                return;
            }
            SetDialogCollection(collection);
            StartDialogue();
            OnDialogChainStart?.Invoke();
        }

        private void StartDialogue()
        {
            DialogueSystemRefs.Instance.CharactorDisplayHandler?.DisplayCharactor(_currentCollection.Dialogs[_dialogueIndex].Charactor);
            _typeWriter.StartTypeWriter(_currentCollection.Dialogs[_dialogueIndex].Dialogue);
        }

        /// <summary>
        /// Sets the dialog collection
        /// </summary>
        /// <param name="collection">The collection of dialogs</param>
        public void SetDialogCollection(DialogCollection collection)
        {
            _currentCollection = collection;
            _dialogueIndex = 0;
        }

        /// <summary>
        /// Playes the next dialog in the set dialog chain
        /// If on last dialoge then invoks the OnCollectionEnded event
        /// </summary>
        public void NextDialogue()
        {
            if(!_typeWriter.IsTyping)
            {
                _dialogueIndex++;
                if(_dialogueIndex >= _currentCollection.Dialogs.Length)
                {
                    if(_currentCollection.FollowUpResponseCollection != null)
                    {
                        if (_currentCollection.DialogChainEvent != null)
                        {
                            _currentCollection.DialogChainEvent.InvokeEvent();
                        }
                        DialogueSystemRefs.Instance.ResponseOptionsHandler.ShowResponseOptions(_currentCollection.FollowUpResponseCollection);
                    }
                    else
                    {
                        if (_currentCollection.DialogChainEvent != null)
                        {
                            _currentCollection.DialogChainEvent.InvokeEvent();
                        }
                        OnCollectionEnded?.Invoke();
                    }
                    return;
                }

                StartDialogue();
            }
        }

        /// <summary>
        /// Let the flow know that the collection has ended
        /// </summary>
        public void InformCollectionEnded()
        {
            OnCollectionEnded?.Invoke();
        }

        /// <summary>
        /// Forcefully end the dialog string that is being played
        /// </summary>
        public void ForceEndCollection()
        {
            _typeWriter.StopTypeWriter();
            OnForceEnded?.Invoke();
        }
    }
}
