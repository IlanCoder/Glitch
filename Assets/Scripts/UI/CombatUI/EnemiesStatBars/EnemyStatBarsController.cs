using Characters;
using UnityEngine;

namespace UI.CombatUI.EnemiesStatBars {
    [RequireComponent(typeof(CanvasGroup))]
    public class EnemyStatBarsController : NpcStatBarsController {
        [HideInInspector] public Camera mainCamera;

        [Header("Visual Offset")]
        [SerializeField] Vector3 offsetFromUIPivot;

        [Header("Timers")]
        [SerializeField] float nonCombatTimer;
        [SerializeField] float combatTimer;

        
        float _tieTime;
        bool _lockOn;
        bool _inCombat;

        public override void TieNewCharacter(CharacterManager newTarget, bool inCombat = false, int damageReceived = 0) {
            _tieTime = Time.time;
            _inCombat = inCombat;
            base.TieNewCharacter(newTarget, inCombat, damageReceived);
        }

        public override bool IsStillVisible() {
            if (!base.IsStillVisible()) return false;
            if (_lockOn) return true;
            if (_inCombat) {
                if (Time.time - _tieTime < combatTimer) return true;
            } else if (Time.time - _tieTime < nonCombatTimer) return true;
            return false;
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
            Vector3 screenPos = mainCamera.WorldToScreenPoint(TiedCharacter.CombatController.UILockOnPivotRef.position) 
                                + offsetFromUIPivot;
            if (screenPos.z > 0) transform.position = screenPos;
        }

        protected override void SetNewHpValue(int newValue) {
            base.SetNewHpValue(newValue);
            _inCombat = true;
        }
    }
}