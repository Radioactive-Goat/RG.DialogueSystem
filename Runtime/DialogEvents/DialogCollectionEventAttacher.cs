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

        [field: SerializeField]
        [Tooltip("If event should be invoked before or after the on end event is triggered")]
        public bool PlayOnBeforeEndedEvent { get; private set; }
        [SerializeField]
        UnityEvent _dialogEvent;

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
