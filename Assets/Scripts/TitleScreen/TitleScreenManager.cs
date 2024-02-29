using UnityEngine;
using WorldManager;

namespace TitleScreen {
    public class TitleScreenManager : MonoBehaviour {
        [SerializeField] WorldSaveManager saveManager;
        public void StartNewGame() {
            StartCoroutine(saveManager.LoadNewGame());
        }
    }
}
