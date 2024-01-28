using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    public interface IDialogSystemEvents
    {
        bool PlayOnBeforeEndedEvent { get; }
        void InvokeEvent();
    }
}
