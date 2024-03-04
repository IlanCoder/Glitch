using UnityEngine;

namespace Characters.Player.PlayerUI {
    public class HUDManager : MonoBehaviour {
        [SerializeField] UIStatBar hpBar;
        [SerializeField] UIStatBar staminaBar;
        [SerializeField] UIStatBar energyBar;

        public void SetNewStaminaValue(float newValue) {
            staminaBar.SetStat(newValue);
        }

        public void SetNewMaxStaminaValue(int newMax) {
            staminaBar.SetMaxStat(newMax);
        }
    }
}
