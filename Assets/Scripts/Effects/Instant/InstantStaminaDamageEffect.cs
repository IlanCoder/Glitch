using UnityEngine;
using Characters;
using Characters.Player;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantStaminaDamage",menuName = "Effects/Instant/Stamina Damage")]
    public class InstantStaminaDamageEffect : InstantCharacterEffect {
        public float staminaDamage;
        
        public override void ProcessEffect(CharacterManager character) {
            if (character.isDead) return;
            if (!character.TryGetComponent(out PlayerManager player)) return;
            CalculateStaminaDamage(player);
        }

        void CalculateStaminaDamage(PlayerManager character) {
            character.statsController.UseStamina(staminaDamage);
        }
    }
}
