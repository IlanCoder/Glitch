using System.Collections.Generic;
using Characters;
using DG.Tweening;
using UI.HUD.UIObjects;
using UnityEngine;

namespace UI.CombatUI {
    [RequireComponent(typeof(CanvasGroup))]
    public class EnemyStatBarsController : MonoBehaviour {
        [HideInInspector] public Camera mainCamera;

        [Header("Visual Offset")]
        [SerializeField] Vector3 offsetFromUIPivot;

        [Header("Timers")]
        [SerializeField] float nonCombatTimer;
        [SerializeField] float combatTimer;

        [Header("Stat Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar energyBar;

        [Header("Fade Out Settings")]
        [SerializeField] float fadeOutTime;
        
        CharacterManager _tiedCharacter;
        CanvasGroup _canvasGroup;
        float _tieTime;
        bool _lockOn;
        bool _inCombat;

        void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void TieNewCharacter(CharacterManager newTarget, bool inCombat = false, int damageReceived = 0) {
            if (_tiedCharacter == newTarget) return;
            if (_tiedCharacter) UnsubscribeToEvents();
            _tiedCharacter = newTarget;
            if (!newTarget) return;
            _canvasGroup.alpha = 1;
            _tieTime = Time.time;
            _inCombat = inCombat;
            
            SubscribeToEvents();
            
            healthBar.SetMaxStat(newTarget.StatsController.MaxHp);
            if (inCombat) healthBar.SetStat(newTarget.StatsController.CurrentHp + damageReceived, false);
            healthBar.SetStat(newTarget.StatsController.CurrentHp, inCombat);
            
            energyBar.SetMaxStat(newTarget.StatsController.MaxEnergy);
            energyBar.SetStat(newTarget.StatsController.CurrentEnergy, inCombat);
        }

        public bool IsStillVisible() {
            if (_tiedCharacter.isDead) return false;
            if (_lockOn) return true;
            if (_inCombat) {
                if (Time.time - _tieTime < combatTimer) return true;
            } else if (Time.time - _tieTime < nonCombatTimer) return true;
            return false;
        }

        public void FadeOut() {
            _canvasGroup.DOFade(0, fadeOutTime).OnComplete(() => Untie());
        }
        
        void Untie() {
            TieNewCharacter(null);
            gameObject.SetActive(false);
        }

        void SubscribeToEvents() {
            _tiedCharacter.StatsController.onMaxHpChange.AddListener(SetNewMaxHpValue);
            _tiedCharacter.StatsController.onHpChange.AddListener(SetNewHpValue);
            
            _tiedCharacter.StatsController.onMaxEnergyChange.AddListener(SetNewMaxEnergyValue);
            _tiedCharacter.StatsController.onEnergyChange.AddListener(SetNewEnergyValue);
        }

        void UnsubscribeToEvents() {
            _tiedCharacter.StatsController.onMaxHpChange.RemoveListener(SetNewMaxHpValue);
            _tiedCharacter.StatsController.onHpChange.RemoveListener(SetNewHpValue);
            
            _tiedCharacter.StatsController.onMaxEnergyChange.RemoveListener(SetNewMaxEnergyValue);
            _tiedCharacter.StatsController.onEnergyChange.RemoveListener(SetNewEnergyValue);
        }
        
        public void LockOn() {
            _lockOn = true;
        }
        
        public void RemoveLockOn() {
            _lockOn = false;
            _tieTime = Time.time;
        }

        public void EnableCombat() {
            _inCombat = true;
        }
        
        void LateUpdate() {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(_tiedCharacter.CombatController.UILockOnPivotRef.position) 
                                + offsetFromUIPivot;
            if (screenPos.z > 0) transform.position = screenPos;
        }

        #region Stat Bars
        void SetNewHpValue(int newValue) {
            healthBar.SetStat(newValue);
            _inCombat = true;
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