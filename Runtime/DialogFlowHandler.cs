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
        public event Action OnDialogChainStart;
        public event Action OnCollectionEnded;
        public event Action OnForceEnded;

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
            CharactorDisplayHandler.Instance?.DisplayCharactor(_currentCollection.Dialogs[_dialogueIndex].Charactor);
            TypeWriter.Instance.StartTypeWriter(_currentCollection.Dialogs[_dialogueIndex].Dialogue);
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
            if(!TypeWriter.Instance.IsTyping)
            {
                _dialogueIndex++;
                if(_dialogueIndex >= _currentCollection.Dialogs.Length)
                {
                    if(_currentCollection.FollowUpResponseCollection != null)
                    {
                        ResponseOptionsHandler.Instance.ShowResponseOptions(_currentCollection.FollowUpResponseCollection);
                    }
                    else
                    {
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
        public void InformCollectionEded()
        {
            OnCollectionEnded?.Invoke();
        }

        /// <summary>
        /// Forcefully end the dialog string that is being played
        /// </summary>
        public void ForceEndCollection()
        {
            TypeWriter.Instance.StopTypeWriter();
            OnForceEnded?.Invoke();
        }
    }
}
