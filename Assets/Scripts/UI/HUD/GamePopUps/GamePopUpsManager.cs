using Characters.Player;
using UnityEngine;

namespace UI.HUD.GamePopUps {
    public class GamePopUpsManager : MonoBehaviour {
        [SerializeField] PlayerManager playerManager;
        [Header("Pop Ups")]
        [SerializeField] GameObject deathPopUp;

        void Awake() {
            playerManager.onPlayerDeath.AddListener(ActivateDeathPopUp);
        }

        public void ActivateDeathPopUp() {
            deathPopUp.SetActive(true);
        }
    }
}
