using System;
using Enums;
using UnityEngine;

namespace DataContainers {
    [Serializable]
    public class DamageStats {
        [SerializeField] float physicalDmg;
        [SerializeField] float photonDmg;
        [SerializeField] float shockDmg;
        [SerializeField] float plasmaDmg;
        float _totalBaseDamage = 1;
        float _damageMultiplier = 1;
        float _totalFilteredDamage = 1;

        #region Stats Getters
        public float TotalDamage => _totalBaseDamage * _damageMultiplier;
        public float TotalFilteredDamage => _totalFilteredDamage * _damageMultiplier;
        #endregion
        
        public void Initialize() {
            SetTotalDamage();
        }
        
        void SetTotalDamage() {
            _totalBaseDamage = physicalDmg + photonDmg + shockDmg + plasmaDmg;
        }

        public void SetDamageMultiplier(float motionMultiplier, float attackMultiplier = 1) {
            _damageMultiplier = motionMultiplier * attackMultiplier;
        }

        public void SetFilteredDamage(DamageTypes damageFilter) {
            float tempFilteredDamage = 0;
            if ((damageFilter & DamageTypes.Physical) != 0) tempFilteredDamage += physicalDmg;
            if ((damageFilter & DamageTypes.Photon) != 0) tempFilteredDamage += photonDmg;
            if ((damageFilter & DamageTypes.Shock) != 0) tempFilteredDamage += shockDmg;
            if ((damageFilter & DamageTypes.Plasma) != 0) tempFilteredDamage += plasmaDmg;
            _totalFilteredDamage = tempFilteredDamage;
        }
    }
}
