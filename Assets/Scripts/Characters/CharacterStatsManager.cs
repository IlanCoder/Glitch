using System;
using Characters.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters {
    public class CharacterStatsManager : MonoBehaviour {
        protected CharacterManager manager;

        public string characterName = "Character";
        
        [Header("Main Stats")]
        
        [Header("Sub Stats")] 
        [SerializeField,Min(0.1f)] protected float staminaRegen = 1;
        [SerializeField, Min(0.1f)] protected float staminaRegenDelay = 1;
        float _staminaRegenTimer;
        
        [HideInInspector] public UnityEvent<float> onStaminaChange;
        [HideInInspector] public UnityEvent<int> onMaxStaminaChange;
        [HideInInspector] public UnityEvent<int> onHpChange;
        [HideInInspector] public UnityEvent<int> onMaxHpChange;
        
        public int MaxStamina { get; protected set; }
        public float CurrentStamina { get; protected set; }
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }

        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
        }

        protected virtual void Update() {
            RegenStamina();
        }

        protected void OnDestroy() {
            onStaminaChange.RemoveAllListeners();
            onMaxStaminaChange.RemoveAllListeners();
            onHpChange.RemoveAllListeners();
            onMaxHpChange.RemoveAllListeners();
        }

        void RegenStamina() {
            if (manager.isPerformingAction) return;
            if (manager.isSprinting) return;
            if (CurrentStamina >= MaxStamina) return;
            if (_staminaRegenTimer > 0) {
                _staminaRegenTimer -= Time.deltaTime;
                return;
            }
            CurrentStamina += staminaRegen * Time.deltaTime;
            onStaminaChange?.Invoke(CurrentStamina);
        }

        public void ReceiveDamage(int dmgReceived) {
            CurrentHp -= dmgReceived;
            onHpChange?.Invoke(CurrentHp);
            if (CurrentHp > 0) return;
            manager.HandleDeathEvent();
        }
        
        public void UseStamina(float staminaUsed) {
            CurrentStamina -= staminaUsed;
            onStaminaChange?.Invoke(CurrentStamina);
            _staminaRegenTimer = staminaRegenDelay;
        }
        
        public bool CanPerformStaminaAction() {
            if (manager.isPerformingAction) return false;
            return CurrentStamina > 0;
        }

        protected void SetMaxStamina(int newMaxStamina) {
            MaxStamina = newMaxStamina;
            onMaxStaminaChange?.Invoke(MaxStamina);
        }

        protected void SetMaxHp(int newMaxHp) {
            MaxHp = newMaxHp;
            onMaxHpChange?.Invoke(MaxHp);
        }
    }
}
