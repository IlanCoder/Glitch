using System;
using Characters.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Characters {
    public class CharacterStatsController : MonoBehaviour {
        protected CharacterManager manager;

        public string characterName = "Character";
        
        [HideInInspector] public UnityEvent<int> onHpChange;
        [HideInInspector] public UnityEvent<int> onMaxHpChange;
        [HideInInspector] public UnityEvent<float> onEnergyChange;
        [HideInInspector] public UnityEvent<int> onMaxEnergyChange;
        
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }
        public int MaxEnergy { get; protected set; }
        public float CurrentEnergy { get; protected set; }

        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
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
            manager.HandleDeathEvent();
        }

        public void GainEnergy(float energyGained) {
            CurrentEnergy += energyGained;
            if (CurrentEnergy > MaxEnergy) CurrentEnergy = MaxEnergy;
            onEnergyChange?.Invoke(CurrentEnergy);
        }

        protected void SetMaxHp(int newMaxHp) {
            MaxHp = newMaxHp;
            onMaxHpChange?.Invoke(MaxHp);
        }

        protected void SetMaxEnergy(int newMaxEnergy) {
            MaxEnergy = newMaxEnergy;
            onMaxEnergyChange?.Invoke(MaxEnergy);
        }
    }
}
