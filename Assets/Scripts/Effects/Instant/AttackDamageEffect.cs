using System;
using Characters;
using UnityEngine;
using DataContainers;
using Enums;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantHealthDamage",menuName = "Effects/Instant/Health Damage")]
    public class AttackDamageEffect : InstantCharacterEffect {
        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation;
        public string damageAnimation;

        [HideInInspector] public CharacterManager characterCausingDamage;
        
        [HideInInspector] public float hitAngle;
        [HideInInspector] public Vector3 contactPoint;

        [HideInInspector] public bool deflected;
        
        #region Damage
        DamageStats _damage;
        float _totalDmg;
        #endregion
        
        #region Energy
        float _baseEnergyGain;
        #endregion
        
        #region Poise
        [HideInInspector] public float poiseDmg;
        bool _poiseIsBroken;
        #endregion

        public void SetEffectDamage(DamageStats dmg) {
            _damage = dmg;
        }

        public void SetEffectEnergyGain(float energy) {
            _baseEnergyGain = energy;
        }

        public override void ProcessEffect(CharacterManager character) {
            if (character.isDead) return;
            if (character.isInvulnerable) return;
            
            if (deflected) {
                try {
                    CharacterDeflectController deflectController = character.GetComponent<CharacterDeflectController>();
                    switch (deflectController.deflectQuality) {
                        case DeflectQuality.Miss: break;
                        case DeflectQuality.Imperfect:
                            deflectController.GainImperfectDeflectEnergy();
                            return;
                        case DeflectQuality.Perfect:
                            deflectController.GainPerfectDeflectEnergy();
                            return;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
                catch {
                    throw new NullReferenceException(
                    $"Deflecting character {character.name} has no DeflectController script attached");
                }
            }
            CalculateHealthDamage(character);
            CalculateEnergyGained();
            PlayDamageVFx(character);
            PlayDamageSFx(character);
            PlayDamageAnimation(character);
        }

        void CalculateHealthDamage(CharacterManager character) {
            _totalDmg = _damage.TotalFilteredDamage;
            if (_totalDmg <= 0) _totalDmg = 1;
            character.StatsController.ReceiveDamage(Mathf.RoundToInt(_totalDmg));
        }

        void CalculateEnergyGained() {
            characterCausingDamage.StatsController.GainEnergy(_baseEnergyGain);
        }

        void PlayDamageVFx(CharacterManager character) {
            character.VFxController.PlayDamageVFx(contactPoint);
        }

        void PlayDamageSFx(CharacterManager character) {
            character.SFxController.PlayDamageSFx();
        }

        void PlayDamageAnimation(CharacterManager character) {
            character.AnimController.PlayStaggerAnimation(hitAngle);
        }
    }
}
