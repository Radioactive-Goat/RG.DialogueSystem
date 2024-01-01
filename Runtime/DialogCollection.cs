using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogCollection", menuName = "Dialog System/Dialog Collection")]
    public class DialogCollection : ScriptableObject
    {
        public DialogData[] Dialogs;
    }

    [System.Serializable ]
    public struct DialogData
    {
        public CharactorIdentifier Charactor;
        [TextArea(1, 6)]
        public string Dialogue;
    }
}
