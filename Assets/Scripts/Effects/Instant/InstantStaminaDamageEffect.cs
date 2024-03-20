using UnityEngine;
using Characters;
using Characters.Player;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantStaminaDamage",menuName = "Effects/Instant/Stamina Damage")]
    public class InstantStaminaDamageEffect : InstantCharacterEffect {
        public float staminaDamage;
        
        public override void ProcessEffect(PlayerManager player) {
            CalculateStaminaDamage(player);
        }

        void CalculateStaminaDamage(PlayerManager player) {
            player.statManager.UseStamina(staminaDamage);
        }
    }
}
