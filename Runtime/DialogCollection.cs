using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    /// <summary>
    /// A collestion of dialoges to create a dialog string
    /// </summary>
    [CreateAssetMenu(fileName = "DialogCollection", menuName = "Dialog System/Dialog Collection", order = 0)]
    public class DialogCollection : ScriptableObject
    {
        public DialogData[] Dialogs;
    }

    /// <summary>
    /// Information needed for each dialog
    /// </summary>
    [System.Serializable ]
    public struct DialogData
    {
        public CharactorIdentifier Charactor;
        [TextArea(1, 6)]
        public string Dialogue;
    }
}
