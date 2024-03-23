using Characters;
using UnityEngine;

namespace UI.HUD.GamePopUps {
    public class GamePopUpsManager : MonoBehaviour {
        [SerializeField] CharacterManager characterManager;
        [Header("Pop Ups")]
        [SerializeField] GameObject deathPopUp;

        void Awake() {
            characterManager.onDeath.AddListener(ActivateDeathPopUp);
        }

        public void ActivateDeathPopUp() {
            deathPopUp.SetActive(true);
        }
    }
}
