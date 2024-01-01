using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RG.DialogueSystem
{
    public class CharactorDisplayHandler : MonoBehaviour
    {
        #region Singleton Setup
        public static CharactorDisplayHandler Instance;
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

        [SerializeField] CharactorData[] _allCharactorsData;
        [SerializeField] TextMeshProUGUI _nameText;

        private Dictionary<CharactorIdentifier, CharactorData> _allCharactors;

        private void Start()
        {
            _allCharactors = new Dictionary<CharactorIdentifier, CharactorData>();
            foreach (CharactorData charectorData in _allCharactorsData)
            {
                _allCharactors[charectorData.CharectorIdentifier] = charectorData;
            }
        }

        public void DisplayCharactor(CharactorIdentifier charactor)
        {
            CharactorData currentCharactor = _allCharactors[charactor];
            if(currentCharactor == null)
            {
                Debug.LogError($"There is no character data filled in for the identifier ({charactor}) provided");
                return;
            }

            _nameText?.SetText(currentCharactor.CharactorName);
            currentCharactor.IconHandler.DisplayCharacter();
        }

        public CharactorData GetCharactorData(CharactorIdentifier charactorIdentifier)
        {
            return _allCharactors[charactorIdentifier];
        }

        public void ResetCharactorDisplays()
        {
            foreach (CharactorData charactor in _allCharactors.Values)
            {
                charactor.IconHandler.HideCharacter(invokeHideEvent: false);
            }
        }
    }
}
