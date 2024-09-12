using System;
using UnityEngine;

namespace DataScriptables {
    [CreateAssetMenu(fileName = "Player Stats",menuName = "Stats/Player Stats")]
    public class PlayerStats : CharacterStats {
        [Header("Player Stats")]
        [SerializeField, Min(1)] protected int maxStamina;
        [SerializeField, Min(0.1f)] protected float staminaRegen = 1;
        [SerializeField, Min(0.1f)] protected float staminaRegenDelay = 1;

        public int MaxStamina {
            get { return maxStamina; }
            set { maxStamina = value; }
        }

        public float StaminaRegen => staminaRegen;
        public float StaminaRegenDelay => staminaRegenDelay;

        public void LoadStats(int hp, int stamina, int energy) {
            maxHp = hp;
            maxStamina = stamina;
            maxEnergy = energy;
        }
    }
}