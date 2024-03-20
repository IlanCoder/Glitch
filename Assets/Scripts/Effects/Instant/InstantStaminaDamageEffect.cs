using UnityEngine;
using Characters;

namespace Effects.Instant {
    [CreateAssetMenu(fileName = "InstantStaminaDamage",menuName = "Effects/Instant/Stamina Damage")]
    public class InstantStaminaDamageEffect : InstantCharacterEffect {
        public float staminaDamage;
        
        public override void ProcessEffect(CharacterManager character) {
            CalculateStaminaDamage(character);
        }

        void CalculateStaminaDamage(CharacterManager character) {
            
        }
    }
}
