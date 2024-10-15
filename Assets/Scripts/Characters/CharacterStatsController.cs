using System;
using Characters.Player;
using DataContainers;
using DataScriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Characters {
    public abstract class CharacterStatsController : MonoBehaviour {
        protected CharacterManager Manager;

        public virtual CharacterStats CharacterStats => null;

        [HideInInspector] public UnityEvent<int> onHpChange;
        [HideInInspector] public UnityEvent<float> onEnergyChange;

        public int CurrentHp { get; protected set; }
        public float CurrentEnergy { get; protected set; }
        public float CurrentPosture { get; protected set; }

        protected float LastPostureDamageTime;

        protected virtual void Awake() {
            Manager = GetComponent<CharacterManager>();
        }

        protected virtual void Start() {
            onHpChange?.Invoke(CurrentHp);
            onEnergyChange?.Invoke(CurrentEnergy);
        }

        protected virtual void Update() {
            RegenPosture();
        }

        protected virtual void OnDestroy() {
            onHpChange.RemoveAllListeners();
        }

        public void ReceivePostureDamage(float damage) {
            CurrentPosture -= damage;
            LastPostureDamageTime = Time.time;
        }
        
        public void ResolveDamage(DamageStats damage, float finalModifier = 1) {
            ReceiveDamage(Mathf.RoundToInt(CharacterStats.Armor.ResolveDamageReduction(damage) * finalModifier));
        }
        
        protected virtual void ReceiveDamage(int dmgReceived) {
            CurrentHp -= dmgReceived;
            onHpChange?.Invoke(CurrentHp);
            if (CurrentHp > 0) return;
            Manager.HandleDeathEvent();
        }

        public void GainEnergy(float energyGained) {
            CurrentEnergy += energyGained;
            if (CurrentEnergy > CharacterStats.MaxHp) CurrentEnergy = CharacterStats.MaxEnergy;
            onEnergyChange?.Invoke(CurrentEnergy);
        }

        public bool CanWithstandPoiseInteraction(float poiseDmg) {
            return poiseDmg < CharacterStats.Poise;
        }
        
        void RegenPosture() {
            if (Manager.isPerformingAction) return;
            if (Manager.isSprinting) return;
            if (CurrentPosture >= CharacterStats.Posture) return;
            if (Time.time - LastPostureDamageTime <= CharacterStats.PostureRegenDelay) return;
            CurrentPosture += CharacterStats.PostureRegen * Time.deltaTime;
            if (CurrentPosture > CharacterStats.Posture) CurrentPosture = CharacterStats.Posture;
        }
    }
}
