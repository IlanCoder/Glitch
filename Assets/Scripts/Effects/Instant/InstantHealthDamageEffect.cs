using Characters;
using UnityEngine;
using Characters.Player;
using Structs;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantHealthDamage",menuName = "Effects/Instant/Health Damage")]
    public class InstantHealthDamageEffect : InstantCharacterEffect {
        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSfx = true;
        public AudioClip damageSfx;
        
        [HideInInspector] public CharacterManager characterCausingDamage;
        
        [HideInInspector] public float hitAngle;
        [HideInInspector] public Vector3 contactPoint;
        
        #region Damage
        public DamageTypes Damage;
        int _totalDmg;
  #endregion
        
        #region Poise
        [HideInInspector] public float poiseDmg;
        bool _poiseIsBroken;
  #endregion

        public void InitializeEffect(DamageTypes dmg) {
            Damage = dmg;
        }
        
        public override void ProcessEffect(CharacterManager player) {
            base.ProcessEffect(player);
            CalculateHealthDamage(player);
        }

        void CalculateHealthDamage(CharacterManager player) {
            _totalDmg = Mathf.RoundToInt(Damage.SlashDmg + Damage.StrikeDmg + Damage.ThrustDmg + Damage.PhotonDmg +
                                         Damage.ShockDmg + Damage.PlasmaDmg);
            if (_totalDmg <= 0) _totalDmg = 1;
            player.StatsManager.ReceiveDamage(_totalDmg);
        }
    }
}
