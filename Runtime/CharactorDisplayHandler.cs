using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RG.DialogueSystem
{
    /// <summary>
    /// Handles the displying and hiding of characters on screen based on 
    /// dialogs being said
    /// </summary>
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

        /// <summary>
        /// This dictionary exist for fast look ups
        /// </summary>
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

        /// <summary>
        /// Displays the required charactor
        /// </summary>
        /// <param name="charactor">The enum identifier of the charactor that needs to be displayed</param>
        public void DisplayCharactor(CharactorIdentifier charactor)
        {
            if(_currentCharactor.CharectorIdentifier != charactor || (_currentCharactor.IconHandler != null && !_currentCharactor.IconHandler.IsActive))
            {
                _currentCharactor.IconHandler?.SetAsActiveListner();
                _currentCharactor = _allCharactors[charactor];
                if (_currentCharactor == null)
                {
                    Debug.LogError($"There is no character data filled in for the identifier ({charactor}) provided");
                    return;
                }
                _nameText?.SetText($"{_currentCharactor.CharactorName}{_nameSuperFix}");
                _currentCharactor.IconHandler?.DisplayCharacter();
            }
        }

        /// <summary>
        /// Get the charactor data of a specific charactor
        /// </summary>
        /// <param name="charactorIdentifier">The enum idntifer of the charactor</param>
        /// <returns>The required charactor data if exists else will return null</returns>
        public CharactorData GetCharactorData(CharactorIdentifier charactorIdentifier)
        {
            return _allCharactors[charactorIdentifier];
        }

        /// <summary>
        /// Turnes off the display of all the characters
        /// </summary>
        public void ResetCharactorDisplays()
        {
            foreach (CharactorData charactor in _allCharactors.Values)
            {
                charactor.IconHandler?.HideCharacter(invokeHideEvent: false);
            }
            _nameText?.SetText("");
        }
    }
}
