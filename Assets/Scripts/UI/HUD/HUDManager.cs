using Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.HUD {
    public class HUDManager : MonoBehaviour {
        [SerializeField] CharacterStatsManager statsManager;
        [Header("HUD Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar staminaBar;
        [SerializeField] UIStatBar energyBar;

        void Awake() {
            statsManager.onMaxStaminaChange.AddListener(SetNewMaxStaminaValue);
            statsManager.onStaminaChange.AddListener(SetNewStaminaValue);
            statsManager.onMaxHpChange.AddListener(SetNewMaxHpValue);
            statsManager.onHpChange.AddListener(SetNewHpValue);
        }

        public void SetNewStaminaValue(float newValue) {
            staminaBar.SetStat(newValue);
        }

        public void SetNewMaxStaminaValue(int newMax) {
            staminaBar.SetMaxStat(newMax);
        }
        
        public void SetNewHpValue(int newValue) {
            healthBar.SetStat(newValue);
        }

        public void SetNewMaxHpValue(int newMax) {
            healthBar.SetMaxStat(newMax);
        }
    }
}
