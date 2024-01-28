using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RG.DialogueSystem
{
    public class ResponseCollectionEventAttacher : MonoBehaviour, IDialogSystemEvents
    {
        [SerializeField] DialogResponseCollection _responseCollection;
        [SerializeField]
        [Tooltip("The indexes of the respones on the response collection that need this event to be added to")]
        uint[] _connectEventTo;

        [field: SerializeField]
        [Tooltip("If event should be invoked before or after the on end request is sent to the dialog collection")]
        public bool PlayOnBeforeEndedEvent { get; private set; }
        [SerializeField]
        UnityEvent _responseEvent;

        private void Start()
        {
            foreach (uint index in _connectEventTo)
            {
                if(index >= _responseCollection.Responses.Length)
                {
                    Debug.LogError("Trying to connect to a none existing response index");
                    continue;
                }
                _responseCollection.Responses[index].DialogResponseEvent = this;
            }
        }

        private void OnDestroy()
        {
            foreach (int index in _connectEventTo)
            {
                _responseCollection.Responses[index].DialogResponseEvent = null;
            }
        }

        public void InvokeEvent()
        {
            _responseEvent?.Invoke();
        }
    }
}
