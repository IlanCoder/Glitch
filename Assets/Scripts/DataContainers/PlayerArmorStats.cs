using System;
using Enums;
using UnityEngine;

namespace DataContainers {
    [Serializable]
    public class PlayerArmorStats : ArmorStats {
        [Header("Base Armor")]
        [SerializeField] float physicalArmor;
        [SerializeField] float photonArmor;
        [SerializeField] float shockArmor;
        [SerializeField] float plasmaArmor;

        protected override float ResolveDamageType(DamageTypes type, float damage) {
            damage -= type switch {
                DamageTypes.Physical => physicalArmor,
                DamageTypes.Photon => photonArmor,
                DamageTypes.Shock => shockArmor,
                DamageTypes.Plasma => plasmaArmor,
                _ => 0
            };
            if (damage <= 0) return 0;
            return base.ResolveDamageType(type, damage);
        }
    }
}