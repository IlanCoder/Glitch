using System;
using DataContainers;
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

        public void ResolveImperfectDeflect(DamageStats damage) {
            GainImperfectDeflectEnergy();
            CalculateChipDamage(damage);
            _manager.AnimController.TriggerDeflectHitAnimation();
            PlayDeflectSFx(DeflectQuality.Imperfect);
        }

        public void ResolvePerfectDeflect(CharacterManager deflectedCharacter, DamageStats stats) {
            GainPerfectDeflectEnergy();
            PlayDeflectSFx(DeflectQuality.Perfect);
            float postureDamage = _manager.StatsController.CharacterStats.PerfectDeflectPostureDamage *
                                  stats.TotalPostureDamage;
            deflectedCharacter.StatsController.ReceivePostureDamage(postureDamage);
        }
        
        void GainImperfectDeflectEnergy() {
            _manager.StatsController.GainEnergy(imperfectDeflectEnergyGain);
        }
        
        void CalculateChipDamage(DamageStats damage) {
            _manager.StatsController.ResolveDamage(damage, imperfectChipDamage);
        }
        
        void PlayDeflectSFx(DeflectQuality deflectQuality) {
            _manager.SFxController.PlayDeflectSFx(deflectQuality);
        }
        
        void GainPerfectDeflectEnergy() {
            _manager.StatsController.GainEnergy(perfectDeflectEnergyGain);
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