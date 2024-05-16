using UnityEngine;
using Characters;
using Characters.Player;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantStaminaDamage",menuName = "Effects/Instant/Stamina Damage")]
    public class InstantStaminaDamageEffect : InstantCharacterEffect {
        public float staminaDamage;
        
        public override void ProcessEffect(CharacterManager character) {
            if(character.isDead) return;
            CalculateStaminaDamage(character);
        }

        void CalculateStaminaDamage(CharacterManager character) {
            character.StatsController.UseStamina(staminaDamage);
        }
    }
}
