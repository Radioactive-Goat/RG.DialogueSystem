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
        [SerializeField] string _nameSuperFix = ":";

        private Dictionary<CharactorIdentifier, CharactorData> _allCharactors;
        private CharactorData _currentCharactor;

        private void Start()
        {
            if(_allCharactorsData.Length > 0)
            {
                _currentCharactor = _allCharactorsData[0];
            }
            else
            {
                Debug.LogError("Character list is empty");
                return;
            }


            _allCharactors = new Dictionary<CharactorIdentifier, CharactorData>();
            foreach (CharactorData charectorData in _allCharactorsData)
            {
                if(_allCharactors.ContainsKey(charectorData.CharectorIdentifier))
                {
                    Debug.LogError($"Charcter with name '{charectorData.CharactorName}' is using the same charactor identifier as another charctor");
                    continue;
                }
                _allCharactors[charectorData.CharectorIdentifier] = charectorData;
            }
        }

        public void DisplayCharactor(CharactorIdentifier charactor)
        {
            if(_currentCharactor.CharectorIdentifier != charactor || !_currentCharactor.IconHandler.IsActive)
            {
                _currentCharactor.IconHandler.SetAsActiveListner();
                _currentCharactor = _allCharactors[charactor];
                if (_currentCharactor == null)
                {
                    Debug.LogError($"There is no character data filled in for the identifier ({charactor}) provided");
                    return;
                }
                _nameText?.SetText($"{_currentCharactor.CharactorName}{_nameSuperFix}");
                _currentCharactor.IconHandler.DisplayCharacter();
            }
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
            _nameText?.SetText("");
        }
    }
}
