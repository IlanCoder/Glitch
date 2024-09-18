using System;
using Enums;
using UnityEngine;

namespace DataContainers {
    [Serializable]
    public class ArmorStats {
        [Header("Percentage Armor")]
        [SerializeField, Range(0, 100)] float physicalPctArmor;
        [SerializeField, Range(0, 100)] float photonPctArmor;
        [SerializeField, Range(0, 100)] float shockPctArmor;
        [SerializeField, Range(0, 100)] float plasmaPctArmor;

        public float ResolveDamageReduction(DamageStats damage) {
            DamageTypes filter = damage.DamageFilter;
            float tempFilteredDamage = 0;
            if ((filter & DamageTypes.Physical) != 0)
                tempFilteredDamage += ResolveDamageType(DamageTypes.Physical, damage.Physical);
            if ((filter & DamageTypes.Photon) != 0)
                tempFilteredDamage += ResolveDamageType(DamageTypes.Photon, damage.Photon);
            if ((filter & DamageTypes.Shock) != 0)
                tempFilteredDamage += ResolveDamageType(DamageTypes.Shock, damage.Shock);
            if ((filter & DamageTypes.Plasma) != 0)
                tempFilteredDamage += ResolveDamageType(DamageTypes.Plasma, damage.Plasma);
            return tempFilteredDamage * damage.Multiplier;
        }

        protected virtual float ResolveDamageType(DamageTypes type, float damage) {
            return type switch {
                DamageTypes.Physical => damage * (1 - physicalPctArmor / 100),
                DamageTypes.Photon => damage * (1 - photonPctArmor / 100),
                DamageTypes.Shock => damage * (1 - shockPctArmor / 100),
                DamageTypes.Plasma => damage * (1 - plasmaPctArmor / 100),
                _ => 0
            };
        }
    }
}