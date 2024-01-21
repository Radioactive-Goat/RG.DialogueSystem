using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    /// <summary>
    /// A collestion of responses
    /// </summary>
    [CreateAssetMenu(fileName = "ResponseCollection", menuName = "Dialog System/Dialog Response Collection", order = 1)]
    public class DialogResponseCollection : ScriptableObject
    {
        public CharactorIdentifier RespondingCharactor;
        public DialogResponseData[] Responses;
    }

    /// <summary>
    /// Information needed for each response
    /// </summary>
    [System.Serializable]
    public struct DialogResponseData
    {
        [TextArea(1, 3)]
        public string Response;
        public DialogCollection FollowUpDialogues;
         
        // System.Action or Unity Event for something custom to happen when responded;
        // But not adding yet as I am not sure how the user will attach this event easily as it lives
        // in a scriptatble object
    }
}
