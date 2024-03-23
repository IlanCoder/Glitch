using TMPro;
using UnityEngine;
using WorldManager;

namespace UI.TitleScreen {
    public class UICharacterSaveSlot : MonoBehaviour {
        [SerializeField] WorldSaveManager saveManager;
        [SerializeField] LoadMenuManager loadManager;
        
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

        public void LoadSaveSlot() {
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

        public void OnSelectSlot() {
            loadManager.SelectCharacterSlotIndex(saveDataIndex);
        }
    }
}
