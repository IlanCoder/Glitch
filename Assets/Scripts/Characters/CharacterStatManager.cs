using System;
using Characters.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters {
    public class CharacterStatManager<T> : MonoBehaviour where T : CharacterManager {
        protected T manager;

        public string characterName = "Character";
        
        [Header("Main Stats")] 
        [SerializeField,Range(1, 99)] protected int endurance = 1;

        [Header("Sub Stats")] 
        [SerializeField,Min(0.1f)] protected float staminaRegen = 1;
        [SerializeField, Min(0.1f)] protected float staminaRegenDelay = 1;
        float _staminaRegenTimer;
        
        [Header("Events")] 
        public UnityEvent<float> onStaminaChange;
        public UnityEvent<int> onMaxStaminaChange;
        public float CurrentStamina { get; protected set; }
        public int MaxStamina { get; protected set; }

        protected virtual void Awake() {
            manager = GetComponent<T>();
        }

        protected virtual void Update() {
            RegenStamina();
        }

        void RegenStamina() {
            if (manager.isPerformingAction) return;
            if (manager.isSprinting) return;
            if (_staminaRegenTimer > staminaRegenDelay) {
                _staminaRegenTimer -= Time.deltaTime;
                return;
            }
            if (CurrentStamina >= MaxStamina) return;
            CurrentStamina += staminaRegen * Time.deltaTime;
            onStaminaChange?.Invoke(CurrentStamina);
        }

        public void UseStamina(float staminaUsed) {
            CurrentStamina -= staminaUsed;
            onStaminaChange?.Invoke(CurrentStamina);
            _staminaRegenTimer = staminaRegenDelay;
        }

        protected void SetMaxStamina(int newMaxStamina) {
            MaxStamina = newMaxStamina;
            onMaxStaminaChange?.Invoke(MaxStamina);
            CurrentStamina = MaxStamina;
            onStaminaChange?.Invoke(CurrentStamina);
        }
    }
}
