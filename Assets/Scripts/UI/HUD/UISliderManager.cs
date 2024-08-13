using Characters;
using UI.HUD.UIObjects;
using UnityEngine;

namespace UI.HUD {
    public class UISliderManager : MonoBehaviour {
        [SerializeField] protected CharacterManager manager;

        [Header("Stat Bars")]
        [SerializeField] protected UIStatBar healthBar;
        [SerializeField] protected UIStatBar energyBar;
        
        protected virtual void Awake() {
            SetStatBarsListeners();
        }
        
        protected virtual void SetStatBarsListeners() {
            manager.StatsController.onMaxHpChange.AddListener(SetNewMaxHpValue);
            manager.StatsController.onHpChange.AddListener(SetNewHpValue);
            
            manager.StatsController.onMaxEnergyChange.AddListener(SetNewMaxEnergyValue);
            manager.StatsController.onEnergyChange.AddListener(SetNewEnergyValue);
        }

        #region Stat Bars
        void SetNewHpValue(int newValue) {
            healthBar.SetStat(newValue);
        }

        void SetNewMaxHpValue(int newMax) {
            healthBar.SetMaxStat(newMax);
        }

        void SetNewEnergyValue(float newValue) {
            energyBar.SetStat(newValue);
        }
        
        void SetNewMaxEnergyValue(int newMax) {
            energyBar.SetMaxStat(newMax);
        }
        #endregion
    }
}