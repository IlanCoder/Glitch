using System;
using Enums;
using UnityEngine;

namespace DataContainers {
    [Serializable]
    public class DamageStats {
        [Header("Health Damage")]
        [SerializeField] float physicalDmg;
        [SerializeField] float photonDmg;
        [SerializeField] float shockDmg;
        [SerializeField] float plasmaDmg;
        DamageTypes _damageFilter;
        
        [Header("Misc Damage")]
        [SerializeField] float poiseDmg;
        [SerializeField] float postureDmg;
        
        float _totalBaseDamage = 1;
        float _poiseMultiplier = 1;

        #region Stats Getters
        public float Physical => physicalDmg;
        public float Photon => photonDmg;
        public float Shock => shockDmg;
        public float Plasma => plasmaDmg;
        public DamageTypes DamageFilter {
            get { return _damageFilter; }
            set { _damageFilter = value; }
        }
        public float Multiplier { get; private set; } = 1;
        public float TotalPoiseDamage => poiseDmg * _poiseMultiplier;
        #endregion
        
        public void Initialize() {
            SetTotalDamage();
        }
        
        void SetTotalDamage() {
            _totalBaseDamage = physicalDmg + photonDmg + shockDmg + plasmaDmg;
        }

        public void SetDamageMultiplier(float motionMultiplier, float attackMultiplier = 1) {
            Multiplier = motionMultiplier * attackMultiplier;
        }

        public void SetPoiseMultiplier(float poiseMultiplier) {
            _poiseMultiplier = poiseMultiplier;
        }
    }
}
