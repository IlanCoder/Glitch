using System;
using Characters.Player;
using DataScriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Characters {
    public class CharacterStatsController : MonoBehaviour {
        protected CharacterManager Manager;

        public virtual CharacterStats CharacterStats => null;

        [HideInInspector] public UnityEvent<int> onHpChange;
        [HideInInspector] public UnityEvent<int> onMaxHpChange;
        [HideInInspector] public UnityEvent<float> onEnergyChange;
        [HideInInspector] public UnityEvent<int> onMaxEnergyChange;
        
        public int CurrentHp { get; protected set; }
        public float CurrentEnergy { get; protected set; }

        protected virtual void Awake() {
            Manager = GetComponent<CharacterManager>();
        }

        protected virtual void Start() {
            onHpChange?.Invoke(CurrentHp);
            onEnergyChange?.Invoke(CurrentEnergy);
        }

        protected virtual void OnDestroy() {
            onHpChange.RemoveAllListeners();
            onMaxHpChange.RemoveAllListeners();
        }

        public virtual void ReceiveDamage(int dmgReceived) {
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

        protected void SetMaxHp(int newMaxHp) {
            CharacterStats.MaxHp = newMaxHp;
            onMaxHpChange?.Invoke(CharacterStats.MaxHp);
        }

        protected void SetMaxEnergy(int newMaxEnergy) {
            CharacterStats.MaxEnergy = newMaxEnergy;
            onMaxEnergyChange?.Invoke(CharacterStats.MaxEnergy);
        }

        public bool CanWithstandPoiseInteraction(float poiseDmg) {
            return poiseDmg < CharacterStats.Poise;
        }
    }
}
