using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.DialogueSystem
{
    [System.Serializable]
    public class CharactorData
    {
        public CharactorIdentifier CharectorIdentifier;
        public string CharactorName;
        public CharactorIconHandler IconHandler;
    }

    public enum CharactorIdentifier
    {
        Character1, Character2, Character3, Character4, Character5, Character6, Character7, Character8,
        Character9, Character10, Character11, Character12, Character13, Character14, Character15, Character16,
    }
}
