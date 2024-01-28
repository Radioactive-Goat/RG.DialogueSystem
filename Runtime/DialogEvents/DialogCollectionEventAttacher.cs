using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace RG.DialogueSystem
{
    public class DialogCollectionEventAttacher : MonoBehaviour, IDialogSystemEvents
    {
        [SerializeField] DialogCollection _connectEventTo;
        [SerializeField] UnityEvent _dialogEvent;

        private void Start()
        {
            _connectEventTo.DialogChainEvent = this;
        }

        private void OnDestroy()
        {
            _connectEventTo.DialogChainEvent = null;          
        }

        public void InvokeEvent()
        {
            _dialogEvent?.Invoke();
        }
    }
}
