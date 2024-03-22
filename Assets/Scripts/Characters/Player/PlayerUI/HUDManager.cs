using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player.PlayerUI {
    public class HUDManager : MonoBehaviour {
        [FormerlySerializedAs("statManager")] [SerializeField] CharacterStatsManager statsManager;
        [Header("HUD Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar staminaBar;
        [SerializeField] UIStatBar energyBar;

        void Awake() {
            statsManager.onMaxStaminaChange.AddListener(SetNewMaxStaminaValue);
            statsManager.onStaminaChange.AddListener(SetNewStaminaValue);
            statsManager.onMaxHpChange.AddListener(SetNewMaxHPValue);
            statsManager.onHpChange.AddListener(SetNewHPValue);
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
