using UnityEngine;
using Characters;
using Characters.Player;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantStaminaDamage",menuName = "Effects/Instant/Stamina Damage")]
    public class InstantStaminaDamageEffect : InstantCharacterEffect {
        public float staminaDamage;
        
        public override void ProcessEffect(CharacterManager player) {
            CalculateStaminaDamage(player);
        }

        void CalculateStaminaDamage(CharacterManager player) {
            player.StatsManager.UseStamina(staminaDamage);
        }
    }
}
