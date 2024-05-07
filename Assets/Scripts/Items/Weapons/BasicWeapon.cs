using System;
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

        public virtual void Awake() {
            Damage.SetDamage(slashDmg, strikeDmg, thrustDmg, photonDmg, shockDmg, plasmaDmg);
        }

        public virtual float GetAttackStaminaCost(AttackType attackType, int comboAttackIndex = 0) {
            _attackStaminaCost = baseStaminaCost;
            switch (attackType) {
                case AttackType.Light: break;
                case AttackType.Heavy: break;
                case AttackType.SuperchargedLight: break;
                case AttackType.SuperchargedHeavy: break;
                case AttackType.Ultimate: break;
            }
            return _attackStaminaCost;
        }
    }
}
