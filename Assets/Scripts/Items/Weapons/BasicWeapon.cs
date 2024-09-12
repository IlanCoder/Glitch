using System;
using Attacks;
using Attacks.Player;
using DataContainers;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Items.Weapons {
    public abstract class BasicWeapon : Item {
        [Header("Model")]
        [SerializeField] protected GameObject weaponPrefab;
        public ref GameObject WeaponPrefab { get { return ref weaponPrefab; } }

        [Header("Dual Wielding")]
        [SerializeField] bool dualWield;
        public bool DualWield { get { return dualWield; } }
        [SerializeField] protected GameObject offhandPrefab;
        public ref GameObject OffhandPrefab { get { return ref offhandPrefab; } }

        [Header("Requirements")]
        [SerializeField] protected int dexReq;
        [SerializeField] protected int strReq;
        [SerializeField] protected int cyberReq;
        [SerializeField] protected int controlReq;
        
        [Header("Damage")]
        public DamageStats damage;

        [Header("Stamina Cost")]
        [SerializeField] protected float baseStaminaCost;
        float _attackStaminaCost;

        [Header("Energy Gains")]
        [SerializeField] protected float baseAttackEnergyGain;

        [Header("Player Combos")]
        [SerializeField] protected PlayerCombo[] combos;
        public PlayerCombo[] Combos => combos;

        public virtual void InitializeWeapon() {
            damage.Initialize();
            InstantiateCombos();
        }

        public virtual void InitializeWeapon(DamageStats damageOverride) {
            damage = damageOverride;
            InitializeWeapon();
        }

        void InstantiateCombos() {
            PlayerCombo[] tempCombos = new PlayerCombo[combos.Length];
            for (int i = 0; i < combos.Length; i++) {
                tempCombos[i] = Instantiate(combos[i]);
                tempCombos[i].InstantiateAttacks();
            }
            combos = tempCombos;
        }

        public virtual float GetAttackStaminaCost(PlayerCombo combo, int comboAttackIndex = 0) {
            try {
                return baseStaminaCost * combo.GetAttackInfo(comboAttackIndex).StaminaCostMultiplier;
            }
            catch {
                Debug.LogError($"{itemName}'s weapon combo indexes out of range to get Stamina Cost");
                return 0;
            }
        }

        public virtual float GetAttackEnergyGain(CharacterAttack attack) {
            try {
                return baseAttackEnergyGain * attack.EnergyGainMultiplier;
            }
            catch {
                Debug.LogError($"{itemName}'s weapon combo indexes out of range to get Energy Gain");
                return 0;
            }
        }

        public virtual void SetAttackDamage(CharacterAttack attack) {
            damage.SetDamageMultiplier(attack.MotionCostMultiplier);
            damage.DamageFilter = attack.AttackDamageTypes;
            damage.SetPoiseMultiplier(attack.PoiseDamageMultiplier);
        }
    }
}
