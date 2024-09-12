using DataScriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player {
    public class PlayerStatsController : CharacterStatsController {
        [SerializeField] protected PlayerStats playerStats;

        public PlayerStats PlayerStats => playerStats;
        public override CharacterStats CharacterStats => playerStats;

        float _lastStaminaActionTime;
        
        [HideInInspector] public UnityEvent<float> onStaminaChange;
        [HideInInspector] public UnityEvent<int> onMaxStaminaChange;
        [HideInInspector] public UnityEvent onStaminaDepletion;
        
        public float CurrentStamina { get; protected set; }

        protected override void Start() {
            SetNewLevel();
        }
        
        void Update() {
            RegenStamina();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            onStaminaChange.RemoveAllListeners();
            onMaxStaminaChange.RemoveAllListeners();
        }

        void SetStats() {
            SetMaxStamina(playerStats.MaxStamina);
            SetMaxHp(playerStats.MaxHp);
            SetMaxEnergy(playerStats.MaxEnergy);
        }

        public void RevivePlayer() {
            CurrentHp = playerStats.MaxHp;
            onHpChange?.Invoke(CurrentHp);
            CurrentStamina = playerStats.MaxStamina;
            onStaminaChange?.Invoke(CurrentStamina);
            CurrentEnergy = 0;
            onEnergyChange?.Invoke(CurrentEnergy);
        }

        public void LoadCharacterStats(int hp, int stamina, int energy) {
            playerStats.LoadStats(hp, stamina, energy);
            SetStats();
        }

        public void LoadCurrentStats(int hp, float stamina, float energy) {
            LoadCurrentHp(hp);
            LoadCurrentStamina(stamina);
            LoadCurrentEnergy(energy);
        }
        
        void LoadCurrentHp(int hp) {
            CurrentHp = hp >= playerStats.MaxHp ? playerStats.MaxHp : hp;
            onHpChange?.Invoke(CurrentHp);
        }

        void LoadCurrentStamina(float stamina) {
            CurrentStamina = stamina >= playerStats.MaxStamina ? playerStats.MaxStamina : stamina;
            onStaminaChange?.Invoke(CurrentStamina);
        }

        void LoadCurrentEnergy(float energy) {
            CurrentEnergy = energy >= playerStats.MaxEnergy ? playerStats.MaxEnergy : energy;
            onEnergyChange?.Invoke(CurrentEnergy);
        }
        
        void RegenStamina() {
            if (Manager.isPerformingAction) return;
            if (Manager.isSprinting) return;
            if (CurrentStamina >= playerStats.MaxStamina) return;
            if (Time.time - _lastStaminaActionTime <= playerStats.StaminaRegenDelay) return;
            CurrentStamina += playerStats.StaminaRegen * Time.deltaTime;
            if (CurrentStamina > playerStats.MaxStamina) CurrentStamina = playerStats.MaxStamina;
            onStaminaChange?.Invoke(CurrentStamina);
        }
        
        public void UseStamina(float staminaUsed) {
            CurrentStamina -= staminaUsed;
            onStaminaChange?.Invoke(CurrentStamina);
            _lastStaminaActionTime = Time.time;
            if (CurrentStamina > 0) return;
            CurrentStamina = 0;
            onStaminaDepletion?.Invoke();
        }
        
        public bool HasStamina() {
            return CurrentStamina > 0;
        }

        protected void SetMaxStamina(int newMaxStamina) {
            playerStats.MaxStamina = newMaxStamina;
            onMaxStaminaChange?.Invoke(playerStats.MaxStamina);
        }

        #region Editor Funcs
#if UNITY_EDITOR
        [ContextMenu("Set New Level")]
        void SetNewLevel() {
            SetStats();
            LoadCurrentStats(playerStats.MaxHp, playerStats.MaxStamina, CurrentEnergy);
        }
#endif
  #endregion
    }
}
