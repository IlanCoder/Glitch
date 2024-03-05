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

        int _currentSaveDataIndex = 0;
        PlayerSaveData _currentSaveData = new PlayerSaveData();
        string _fileName;
        
        public List<PlayerSaveData> CharacterSlots = new List<PlayerSaveData>(10);
        
        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        void GetSaveFileNameBasesOnIndex() {
            _fileName = $"CharacterSlot_{_currentSaveDataIndex}";
        }

        public void CreateNewGame() {
            GetSaveFileNameBasesOnIndex();
            _currentSaveData = new PlayerSaveData();
        }

        [ContextMenu("Load")]
        public void LoadGame() {
            GetSaveFileNameBasesOnIndex();
            #if UNITY_EDITOR
            _saveFileEditor = new SaveFileEditor(Application.dataPath, _fileName);
            #else
            _saveFileEditor = new SaveFileEditor(Application.persistentDataPath, _fileName);
            #endif
            _currentSaveData = _saveFileEditor.LoadSaveFile();
            player.LoadPlayerData(ref _currentSaveData);
            StartCoroutine(LoadWorld());
        }
        [ContextMenu("Save")]
        public void SaveGame() {
            GetSaveFileNameBasesOnIndex();
            #if UNITY_EDITOR
            _saveFileEditor = new SaveFileEditor(Application.dataPath, _fileName);
            #else
            _saveFileEditor = new SaveFileEditor(Application.persistentDataPath, _fileName);
            #endif
            player.SavePlayerData(ref _currentSaveData);
            _saveFileEditor.CreateFile(_currentSaveData);
        }
        
        public IEnumerator LoadWorld() {
            SceneManager.LoadSceneAsync(GameSceneIndex);
            yield return null;
        }
        
        public int GetSceneIndex() {
            return GameSceneIndex;
        }
    }
}
