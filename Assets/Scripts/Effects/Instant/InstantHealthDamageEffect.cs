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
        public DamageValues Damage;
        float _totalDmg;
  #endregion
        
        #region Poise
        [HideInInspector] public float poiseDmg;
        bool _poiseIsBroken;
  #endregion

        public void SetEffectDamage(DamageValues dmg) {
            Damage = dmg;
        }
        
        public override void ProcessEffect(CharacterManager character) {
            if(character.isDead) return;
            CalculateHealthDamage(character);
            PlayDamageVFx(character);
        }

        void CalculateHealthDamage(CharacterManager character) {
            _totalDmg = Damage.TotalMultipliedDamage;
            if (_totalDmg <= 0) _totalDmg = 1;
            character.StatsManager.ReceiveDamage(Mathf.RoundToInt(_totalDmg));
        }

        void PlayDamageVFx(CharacterManager character) {
            character.VFxManager.PlayDamageVFx(contactPoint);
        }

        void PlayDamageSFx(CharacterManager character) {
            
        }
    }
}
