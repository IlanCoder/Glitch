using System;
using System.Globalization;
using SaveSystem;
using UnityEngine;
using TMPro;
using WorldManager;

namespace TitleScreen {
    public class UICharacterSaveSlot : MonoBehaviour {
        [SerializeField] WorldSaveManager saveManager;
        
        [SerializeField] int saveDataIndex;

        [Header("Character Info")]
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] TextMeshProUGUI timePlayed;

        void OnEnable() {
            LoadSaveSlot();
        }

        void SetUI() {
            characterName.text = saveManager.CharacterSlots[saveDataIndex].PlayerName;
            timePlayed.text = saveManager.CharacterSlots[saveDataIndex].TimePlayed.ToString();
        }

        void LoadSaveSlot() {
            if (saveManager.CharacterSlots[saveDataIndex] == null) {
                gameObject.SetActive(false);
                return;
            }
            SetUI();
        }

        public void LoadGame() {
            saveManager.currentSaveDataIndex = saveDataIndex;
            saveManager.LoadGame();
        }
    }
}
