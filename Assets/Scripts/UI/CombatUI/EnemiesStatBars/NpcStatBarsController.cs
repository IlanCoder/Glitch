using Characters;
using DG.Tweening;
using UI.HUD.UIObjects;
using UnityEngine;

namespace UI.CombatUI.EnemiesStatBars {
    public abstract class NpcStatBarsController : MonoBehaviour{
        [Header("Stat Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar energyBar;

        [Header("Fade Out Settings")]
        [SerializeField] float fadeOutTime;
        
        public CharacterManager TiedCharacter { get; protected set; }
        CanvasGroup _canvasGroup;

        void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public virtual void TieNewCharacter(CharacterManager newTarget, bool inCombat = false, int damageReceived = 0) {
            if (TiedCharacter == newTarget) return;
            if (TiedCharacter) UnsubscribeToEvents();
            TiedCharacter = newTarget;
            if (!newTarget) return;
            _canvasGroup.alpha = 1;

            SubscribeToEvents();
            
            healthBar.SetMaxStat(newTarget.StatsController.MaxHp);
            if (inCombat) healthBar.SetStat(newTarget.StatsController.CurrentHp + damageReceived, false);
            healthBar.SetStat(newTarget.StatsController.CurrentHp, inCombat);
            
            energyBar.SetMaxStat(newTarget.StatsController.MaxEnergy);
            energyBar.SetStat(newTarget.StatsController.CurrentEnergy, inCombat);
        }
        
        public void FadeOut() {
            _canvasGroup.DOFade(0, fadeOutTime).OnComplete(() => Untie());
        }
        
        void Untie() {
            TieNewCharacter(null);
            gameObject.SetActive(false);
        }

        public virtual bool IsStillVisible() {
            if (!TiedCharacter) return false;
            if (TiedCharacter.isDead) return false;
            return true;
        }
        
        void SubscribeToEvents() {
            TiedCharacter.StatsController.onMaxHpChange.AddListener(SetNewMaxHpValue);
            TiedCharacter.StatsController.onHpChange.AddListener(SetNewHpValue);
            
            TiedCharacter.StatsController.onMaxEnergyChange.AddListener(SetNewMaxEnergyValue);
            TiedCharacter.StatsController.onEnergyChange.AddListener(SetNewEnergyValue);
        }

        void UnsubscribeToEvents() {
            TiedCharacter.StatsController.onMaxHpChange.RemoveListener(SetNewMaxHpValue);
            TiedCharacter.StatsController.onHpChange.RemoveListener(SetNewHpValue);
            
            TiedCharacter.StatsController.onMaxEnergyChange.RemoveListener(SetNewMaxEnergyValue);
            TiedCharacter.StatsController.onEnergyChange.RemoveListener(SetNewEnergyValue);
        }
        
        #region Stat Bars
        protected virtual void SetNewHpValue(int newValue) {
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