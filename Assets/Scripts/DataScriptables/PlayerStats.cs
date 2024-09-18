using System;
using DataContainers;
using UnityEngine;
using UnityEngine.Events;

namespace DataScriptables {
    [CreateAssetMenu(fileName = "Player Stats",menuName = "Stats/Player Stats")]
    public class PlayerStats : CharacterStats {
        [Header("Player Stats")]
        [SerializeField, Min(1)] protected int maxStamina;
        [SerializeField, Min(0.1f)] protected float staminaRegen = 1;
        [SerializeField, Min(0.1f)] protected float staminaRegenDelay = 1;
        
        [Header("Combat Stats")]
        [SerializeField] PlayerArmorStats armor;

        public int MaxStamina => maxStamina;
        public float StaminaRegen => staminaRegen;
        public float StaminaRegenDelay => staminaRegenDelay;
        override public ArmorStats Armor => armor;
        
        [HideInInspector] public UnityEvent<int> onMaxStaminaChange;

        public void LoadStats(int hp, int stamina, int energy) {
            SetMaxHp(hp);
            SetMaxStamina(stamina);
            SetMaxEnergy(energy);
        }
        
        public void SetMaxStamina(int newMaxStamina) {
            maxStamina = newMaxStamina;
            onMaxStaminaChange?.Invoke(maxStamina);
        }
    }
}