using System;
using Attacks;
using DataContainers;
using Enums;
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
        [SerializeField] protected float slashDmg;
        [SerializeField] protected float strikeDmg;
        [SerializeField] protected float thrustDmg;
        [SerializeField] protected float photonDmg;
        [SerializeField] protected float shockDmg;
        [SerializeField] protected float plasmaDmg;
        public DamageValues Damage { get; protected set; } = new DamageValues();
        
        [Header("Poise & Posture")]
        [SerializeField] protected float poiseDmg;
        [SerializeField] protected float postureDmg;

        [Header("Stamina Cost")]
        [SerializeField] protected float baseStaminaCost;
        float _attackStaminaCost;

        [Header("Combos")]
        [SerializeField] protected PlayerCombo[] combos;
        public PlayerCombo[] Combos { get { return combos; } }

        public virtual void Awake() {
            Damage.SetDamage(slashDmg, strikeDmg, thrustDmg, photonDmg, shockDmg, plasmaDmg);
        }

        public virtual float GetAttackStaminaCost(int comboIndex, int comboAttackIndex = 0) {
            return baseStaminaCost * combos[comboIndex].GetAttackInfo(comboAttackIndex).StaminaCostMultiplier;
        }

        public virtual float GetAttackMotionMultiplier(PlayerCombo combo, int comboAttackIndex = 0) {
            return combo.GetAttackInfo(comboAttackIndex).MotionCostMultiplier;
        }

        public AnimationClip GetAttackAnimation(int comboIndex, int comboAttackIndex = 0) {
            return combos[comboIndex].GetAttackInfo(comboAttackIndex).AttackAnimation;
        }
    }
}
