using System;
using DataContainers;
using UnityEngine;
using UnityEngine.Events;

namespace DataScriptables {
    [Serializable]
    public abstract class CharacterStats : ScriptableObject {
        [Header("Name")]
        [SerializeField] protected string characterName;

        [Header("Basic Stats")]
        [SerializeField, Min(1)] protected int maxHp;
        [SerializeField, Min(1)] protected int maxEnergy;

        [Header("Poise & Posture")]
        [SerializeField, Min(0)] float poise;
        [SerializeField, Min(0)] float posture;
        [SerializeField, Min(0)] float postureRegen;
        [SerializeField, Min(0)] float postureRegenDelay;

        [Header("Deflect")]
        [SerializeField, Range(0.1f, 1f)] float perfectDeflectPostureDamage;

        public string CharacterName {
            get { return characterName; }
            set { characterName = value; }
        }
        public int MaxHp => maxHp;
        public int MaxEnergy => maxEnergy;
        public float Poise => poise;
        public float Posture => posture;
        public float PostureRegen => postureRegen;
        public float PostureRegenDelay => postureRegenDelay;
        public float PerfectDeflectPostureDamage => perfectDeflectPostureDamage;
        public virtual ArmorStats Armor => null;
        
        [HideInInspector] public UnityEvent<int> onMaxHpChange;
        [HideInInspector] public UnityEvent<int> onMaxEnergyChange;
        
        public void SetMaxHp(int newMaxHp) {
            maxHp = newMaxHp;
            onMaxHpChange?.Invoke(maxHp);
        }

        public void SetMaxEnergy(int newMaxEnergy) {
            maxEnergy = newMaxEnergy;
            onMaxEnergyChange?.Invoke(maxEnergy);
        }
    }
}