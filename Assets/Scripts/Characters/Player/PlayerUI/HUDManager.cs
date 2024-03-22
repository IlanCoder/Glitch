using System;
using UnityEngine;

namespace Characters.Player.PlayerUI {
    public class HUDManager : MonoBehaviour {
        [SerializeField] CharacterStatManager<PlayerManager> statManager;
        [Header("HUD Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar staminaBar;
        [SerializeField] UIStatBar energyBar;

        void Awake() {
            statManager.onMaxStaminaChange.AddListener(SetNewMaxStaminaValue);
            statManager.onStaminaChange.AddListener(SetNewStaminaValue);
            statManager.onMaxHpChange.AddListener(SetNewMaxHPValue);
            statManager.onHpChange.AddListener(SetNewHPValue);
        }

        public void SetNewStaminaValue(float newValue) {
            staminaBar.SetStat(newValue);
        }

        public void SetNewMaxStaminaValue(int newMax) {
            staminaBar.SetMaxStat(newMax);
        }
        
        public void SetNewHPValue(float newValue) {
            healthBar.SetStat(newValue);
        }

        public void SetNewMaxHPValue(int newMax) {
            healthBar.SetMaxStat(newMax);
        }
    }
}
