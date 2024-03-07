using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldManager {
    public class WorldSaveManager : MonoBehaviour {
        const int GameSceneIndex = 1;

        [SerializeField] PlayerManager player;
        SaveFileEditor _saveFileEditor;

        public int currentSaveDataIndex;
        PlayerSaveData _currentSaveData = new PlayerSaveData();
        string _fileName;

        public PlayerSaveData[] CharacterSlots { get; private set; } = new PlayerSaveData[10];

        void Awake() {
            LoadWorldManager();
        }

        public void LoadWorldManager() {
            #if UNITY_EDITOR
            _saveFileEditor = new SaveFileEditor(Application.dataPath + "/SaveFiles", _fileName);
            #else
            _saveFileEditor = new SaveFileEditor(Application.persistentDataPath + "/SaveFiles", _fileName);
            #endif
            LoadAllSlots();
        }

        void LoadAllSlots() {
            for (int i = 0; i < CharacterSlots.Length; i++) {
                ChangeSaveFileBaseOnIndex(i);
                if (!_saveFileEditor.CheckFileExists()) {
                    CharacterSlots[i] = null;
                    continue;
                }
                CharacterSlots[i] = _saveFileEditor.LoadSaveFile();
            }
        }

        void ChangeSaveFileBaseOnIndex(int index) {
            _fileName = GetSaveFileNameBasesOnIndex(index);
            _saveFileEditor.ChangeFile(_fileName);
        }

        public string GetSaveFileNameBasesOnIndex(int saveIndex) {
            return $"CharacterSlot_{saveIndex}";
        }

        public bool AttemptToCreateNewGame() {
            for (int i = 0; i < CharacterSlots.Length; i++) {
                ChangeSaveFileBaseOnIndex(i);
                if (_saveFileEditor.CheckFileExists()) continue;
                currentSaveDataIndex = i;
                CharacterSlots[i] = new PlayerSaveData();
                _currentSaveData = CharacterSlots[i];
                StartCoroutine(LoadWorld());
                return true;
            }
            return false;
        }

        [ContextMenu("Load")]
        public void LoadGame() {
            ChangeSaveFileBaseOnIndex(currentSaveDataIndex);
            _currentSaveData = _saveFileEditor.LoadSaveFile();
            StartCoroutine(LoadWorld());
        }
        [ContextMenu("Save")]
        public void SaveGame() {
            ChangeSaveFileBaseOnIndex(currentSaveDataIndex);
            player.SavePlayerData(ref _currentSaveData);
            _saveFileEditor.SaveFile(_currentSaveData);
        }

        public void DeleteGame(int gameIndex) {
            ChangeSaveFileBaseOnIndex(gameIndex);
            CharacterSlots[gameIndex] = null;
            _saveFileEditor.DeleteFile();
        }
        
        public IEnumerator LoadWorld() {
            SceneManager.LoadSceneAsync(GameSceneIndex);
            player.LoadPlayerData(ref _currentSaveData);
            yield return null;
        }
        
        public int GetSceneIndex() {
            return GameSceneIndex;
        }
    }
}
