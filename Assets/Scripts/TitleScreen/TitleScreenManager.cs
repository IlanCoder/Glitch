using UnityEngine;
using WorldManager;

namespace TitleScreen {
    public class TitleScreenManager : MonoBehaviour {
        [SerializeField] WorldSaveManager saveManager;
        public void StartNewGame() {
            saveManager.CreateNewGame();
            StartCoroutine(saveManager.LoadWorld());
        }
    }
}
