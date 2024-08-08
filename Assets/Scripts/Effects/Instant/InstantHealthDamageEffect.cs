using Characters;
using UnityEngine;
using Characters.Player;
using DataContainers;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantHealthDamage",menuName = "Effects/Instant/Health Damage")]
    public class InstantHealthDamageEffect : InstantCharacterEffect {
        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation;
        public string damageAnimation;

        [HideInInspector] public CharacterManager characterCausingDamage;
        
        [HideInInspector] public float hitAngle;
        [HideInInspector] public Vector3 contactPoint;
        
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
            if(character.isDead) return;
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
