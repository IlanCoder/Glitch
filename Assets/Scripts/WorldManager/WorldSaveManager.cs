using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldManager {
    public class WorldSaveManager : MonoBehaviour {
        const int GameSceneIndex = 1;

        public int GetSceneIndex() {
            return GameSceneIndex;
        }

        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator LoadNewGame() {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(GameSceneIndex);
            yield return null;
        }
    }
}
