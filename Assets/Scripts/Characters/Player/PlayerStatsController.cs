using System;
using DataContainers;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace Characters.Player {
    public class PlayerStatsController : CharacterStatsController {
        [SerializeField] protected PlayerAttributes playerAttributes;
        
        [Header("Sub Stats")] 
        [SerializeField, Min(0.1f)] protected float staminaRegen = 1;
        [SerializeField, Min(0.1f)] protected float staminaRegenDelay = 1;
        float _staminaRegenTimer;
        
        [HideInInspector] public UnityEvent<float> onStaminaChange;
        [HideInInspector] public UnityEvent<int> onMaxStaminaChange;
        
        public int MaxStamina { get; protected set; }
        public float CurrentStamina { get; protected set; }

        #region Attributes Gets
        public int Vitality => playerAttributes.vitality;
        public int Endurance => playerAttributes.endurance;
        public int Dexterity => playerAttributes.dexterity;
        public int Strength => playerAttributes.strength;
        public int Cyber => playerAttributes.cyber;
        public int Control => playerAttributes.control;
  #endregion

        void Start() {
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

        void SetStatsBasedOnAttributes() {
            SetMaxStamina(SetStaminaBasedOnLevel());
            SetMaxHp(SetHPBasedOnLevel());
            SetMaxEnergy(100);
        }
        
        int SetStaminaBasedOnLevel() {
            return playerAttributes.endurance switch {
                <= 15 => Mathf.FloorToInt(80 + 25 * (playerAttributes.endurance - 1) / 14),
                <= 35 => Mathf.FloorToInt(105 + 25 * (playerAttributes.endurance - 15) / 15),
                <= 60 => Mathf.FloorToInt(130 + 25 * (playerAttributes.endurance - 30) / 20),
                _ => Mathf.FloorToInt(155 + 15 * (playerAttributes.endurance - 59) / 49),
            };
        }

        int SetHPBasedOnLevel() {
            return playerAttributes.vitality switch {
                <= 25 => Mathf.FloorToInt(300 + 500 * Mathf.Pow((playerAttributes.vitality - 1) / 24f, 1.5f)),
                <= 40 => Mathf.FloorToInt(800 + 650 * Mathf.Pow((playerAttributes.vitality - 25) / 15f, 1.1f)),
                <= 60 => Mathf.FloorToInt(1450 + 450 * (1 - Mathf.Pow(1 - (playerAttributes.vitality - 40) / 20f, 1.2f))),
                _ => Mathf.FloorToInt(1900 + 200 * (1 - Mathf.Pow(1 - (playerAttributes.vitality - 60) / 39f, 1.2f)))
            };
        }

        public void RevivePlayer() {
            CurrentHp = MaxHp;
            CurrentStamina = MaxStamina;
        }

        public void LoadCharacterAttributes(int vit, int end, int dex, int str, int cbr, int ctrl) {
            playerAttributes.vitality = vit;
            playerAttributes.endurance = end;
            playerAttributes.dexterity = dex;
            playerAttributes.strength = str;
            playerAttributes.cyber = cbr;
            playerAttributes.control = ctrl;
            SetStatsBasedOnAttributes();
        }

        public void LoadCurrentStats(int hp, float stamina, float energy) {
            LoadCurrentHp(hp);
            LoadCurrentStamina(stamina);
            LoadCurrentEnergy(energy);
        }
        
        void LoadCurrentHp(int hp) {
            CurrentHp = hp >= MaxHp ? MaxHp : hp;
            onHpChange?.Invoke(CurrentHp);
        }

        void LoadCurrentStamina(float stamina) {
            CurrentStamina = stamina >= MaxStamina ? MaxStamina : stamina;
            onStaminaChange?.Invoke(CurrentStamina);
        }

        void LoadCurrentEnergy(float energy) {
            CurrentEnergy = energy >= MaxEnergy ? MaxEnergy : energy;
            onEnergyChange?.Invoke(CurrentEnergy);
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
            if (CurrentStamina > MaxStamina) CurrentStamina = MaxStamina;
            onStaminaChange?.Invoke(CurrentStamina);
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

        #region Editor Funcs
#if UNITY_EDITOR
        [ContextMenu("Set New Level")]
        void SetNewLevel() {
            SetStatsBasedOnAttributes();
            LoadCurrentStats(MaxHp, MaxStamina, CurrentEnergy);
        }
#endif
  #endregion
    }
}
