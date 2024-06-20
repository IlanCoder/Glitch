using System;
using UnityEngine;

namespace DataContainers {
    [Serializable]
    public class DamageStats {
        [SerializeField] float slashDmg;
        [SerializeField] float strikeDmg;
        [SerializeField] float thrustDmg;
        [SerializeField] float photonDmg;
        [SerializeField] float shockDmg;
        [SerializeField] float plasmaDmg;
        float _totalBaseDamage = 1;
        float _damageMultiplier = 1;
        
        #region Stats Getters
        public float TotalDamage => _totalBaseDamage * _damageMultiplier;
        #endregion
        
        public void Initialize() {
            SetTotalDamage();
        }
        
        void SetTotalDamage() {
            _totalBaseDamage = slashDmg + strikeDmg + thrustDmg + photonDmg + shockDmg + plasmaDmg;
        }

        public void SetDamageMultiplier(float motionMultiplier, float attackMultiplier = 1) {
            _damageMultiplier = motionMultiplier * attackMultiplier;
        }
    }
}
