using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    public class DialogueSystemRefs : MonoBehaviour
    {
        #region Singleton Setup
        public static DialogueSystemRefs Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        #endregion

        [field: SerializeField]
        public DialogFlowHandler DialogFlowHandler { get; private set; }

        [field: SerializeField]
        public CharactorDisplayHandler CharactorDisplayHandler { get; private set; }

        [field: SerializeField]
        public ResponseOptionsHandler ResponseOptionsHandler { get; private set; }

        [field: SerializeField]
        public TypeWriter TypeWriter { get; private set; }
    }
}
