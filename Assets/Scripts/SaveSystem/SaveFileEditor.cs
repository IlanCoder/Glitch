using UnityEngine;
using System;
using System.IO;

namespace SaveSystem {
    public class SaveFileEditor {
        string _saveDataPath;
        string _saveFileName;

        public SaveFileEditor(string dataPath, string fileName) {
            _saveDataPath = dataPath;
            _saveFileName = fileName;
        }
        
        public bool CheckFileExists() {
            return File.Exists(Path.Combine(_saveDataPath, _saveFileName));
        }

        public void DeleteFile() {
            File.Delete(Path.Combine(_saveDataPath, _saveFileName));
        }

        public void SaveFile(PlayerSaveData playerData) {
            string savePath = Path.Combine(_saveDataPath, _saveFileName);
            try {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                string dataToStore = JsonUtility.ToJson(playerData, true);

                FileStream stream = new FileStream(savePath, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(dataToStore);
                writer.Close();
                stream.Close();
            } catch (Exception ex) {
                Debug.LogError($"Error whilst trying to save player data\n{ex}");
            }
        }

        public PlayerSaveData LoadSaveFile() {
            PlayerSaveData playerData = null;
            string loadPath = Path.Combine(_saveDataPath, _saveFileName);
            if (!File.Exists(loadPath)) return null;
            try {
                FileStream stream = new FileStream(loadPath, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string dataToLoad = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                
                playerData = JsonUtility.FromJson<PlayerSaveData>(dataToLoad);
            }
            catch (Exception ex) {
                Debug.LogError($"Error whilst trying to load player data\n{ex}");
            }
            return playerData;
        }
    }
}
