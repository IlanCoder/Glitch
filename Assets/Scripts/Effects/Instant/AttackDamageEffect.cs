using System;
using Characters;
using UnityEngine;
using DataContainers;
using Enums;
using Unity.VisualScripting;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantHealthDamage",menuName = "Effects/Instant/Health Damage")]
    public class AttackDamageEffect : InstantCharacterEffect {
        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation;
        public AnimationClip damageAnimation;

        [Header("Deflect Settings")]
        [SerializeField, Range(0.01f, 1), Tooltip("% of attack's energy gain when attack is imperfectly deflected")]
            float deflectEnergyModifier;

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
                            ResolveImperfectDeflect(character, deflectController);
                            return;
                        case DeflectQuality.Perfect:
                            deflectController.GainPerfectDeflectEnergy();
                            PlayDeflectSFx(character, DeflectQuality.Perfect);
                            return;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
                catch {
                    throw new NullReferenceException(
                    $"Deflecting character {character.name} has no DeflectController script attached");
                }
            }
            ResolveFullAttack(character);
        }

        void ResolveImperfectDeflect(CharacterManager character, CharacterDeflectController deflectController) {
            deflectController.GainImperfectDeflectEnergy();
            CalculateHealthDamage(character);
            deflectController.CalculateChipDamage(_totalDmg);
            PlayDeflectSFx(character, DeflectQuality.Imperfect);
            character.AnimController.TriggerDeflectHitAnimation();
            CalculateEnergyGained(deflectEnergyModifier);
        }

        void ResolveFullAttack(CharacterManager character) {
            CalculateHealthDamage(character);
            character.StatsController.ReceiveDamage(Mathf.RoundToInt(_totalDmg));
            CalculateEnergyGained();
            PlayDamageSFx(character);

            PlayDamageVFx(character);
            PlayDamageAnimation(character);
        }

        void CalculateHealthDamage(CharacterManager character) {
            _totalDmg = _damage.TotalFilteredDamage;
            if (_totalDmg <= 0) _totalDmg = 1;
        }

        void CalculateEnergyGained(float deflectModifier = 1) {
            characterCausingDamage.StatsController.GainEnergy(_baseEnergyGain * deflectModifier);
        }

        void PlayDamageVFx(CharacterManager character) {
            character.VFxController.PlayDamageVFx(contactPoint);
        }

        void PlayDamageSFx(CharacterManager character) {
            character.SFxController.PlayDamageSFx();
        }
        
        void PlayDeflectSFx(CharacterManager character, DeflectQuality deflectQuality) {
            character.SFxController.PlayDeflectSFx(deflectQuality);
        }
        
        void PlayDamageAnimation(CharacterManager character) {
            character.AnimController.PlayStaggerAnimation(hitAngle);
        }
    }
}
