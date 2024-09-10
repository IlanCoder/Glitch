using System;
using Enums;
using UnityEngine;

namespace Characters {
    public class CharacterDeflectController : MonoBehaviour {
        CharacterManager _manager;
        
        [SerializeField] protected GameObject deflectCollider;

        [Header("Deflect Settings")]
        [SerializeField] protected float perfectDeflectEnergyGain;
        [Space(5)]
        [SerializeField] protected float imperfectDeflectEnergyGain;
        [SerializeField, Range(0.01f,1f), Tooltip("% of the attack's damage")] protected float imperfectChipDamage;

        [HideInInspector] public DeflectQuality deflectQuality = DeflectQuality.Miss;

        protected void Awake() {
            _manager = GetComponent<CharacterManager>();
        }

        public void TryDeflect(bool deflecting) {
            if (!deflecting) {
                _manager.AnimController.SetDeflectHeldBool(false);
                return;
            }
            if (_manager.isPerformingAction) return;
            _manager.AnimController.SetDeflectHeldBool(true);
        }

        public void GainPerfectDeflectEnergy() {
            _manager.StatsController.GainEnergy(perfectDeflectEnergyGain);
        }
        
        public void GainImperfectDeflectEnergy() {
            _manager.StatsController.GainEnergy(imperfectDeflectEnergyGain);
        }

        public void CalculateChipDamage(float totalDamage) {
            int chipDamage = Mathf.RoundToInt(totalDamage * imperfectChipDamage);
            _manager.StatsController.ReceiveDamage(chipDamage);
        }

        #region Aniamtion Events
        public void EnableDeflectCollider() {
            deflectCollider.SetActive(true);
        }

        public void DisableDeflectCollider() {
            deflectCollider.SetActive(false);
        }
        #endregion
    }
}