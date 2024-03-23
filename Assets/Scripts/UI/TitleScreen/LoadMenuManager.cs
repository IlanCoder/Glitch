using UnityEngine;
using UnityEngine.UI;
using WorldManager;

namespace UI.TitleScreen {
    public class LoadMenuManager : MonoBehaviour {
        PlayerControls _playerControls;
        [SerializeField] WorldSaveManager saveManager;
        
        [Header("Buttons")]
        [SerializeField] Button loadMenuDefaultSelectedButton;
        
        [Header("Pop Ups")]
        [SerializeField] GameObject deleteCharacterPopUp;
        [SerializeField] Button deleteCharacterButton;

        [Header("Save Slots")]
        [SerializeField] UICharacterSaveSlot[] saveSlots = new UICharacterSaveSlot[10];
        
        int _characterSlotIndex = -1;

        void Awake() {
            if (_playerControls != null) return;
            _playerControls = new PlayerControls();
            _playerControls.UI.SecondarySelect.performed += i => AttemptToDeleteCharacter();
        }

        void OnEnable() {
            _playerControls.Enable();
            loadMenuDefaultSelectedButton.Select();
        }

        void OnDisable() {
            _playerControls.Disable();
        }

        public void SelectCharacterSlotIndex(int newIndex) {
            _characterSlotIndex = newIndex;
        }

        public void DeselectCharacterSlot() {
            _characterSlotIndex = -1;
        }
        
        public void DeleteCharacterSlot() {
            saveManager.DeleteGame(_characterSlotIndex);
            saveSlots[_characterSlotIndex].LoadSaveSlot();
            CloseDeleteCharacterWindow();
        }

        void AttemptToDeleteCharacter() {
            if (_characterSlotIndex < 0) return;
            deleteCharacterPopUp.SetActive(true);
            deleteCharacterButton.Select();
        }
        
        public void CloseDeleteCharacterWindow() {
            deleteCharacterPopUp.SetActive(false);
            loadMenuDefaultSelectedButton.Select();
            DeselectCharacterSlot();
        }
    }
}
