using UnityEngine;
using Characters;

namespace Effects.Instant {
    public class InstantStaminaDamageEffect : InstantCharacterEffect {
        public float staminaDamage;
        
        public override void ProcessEffect(CharacterManager character) {
            CalculateStaminaDamage(character);
        }

        void CalculateStaminaDamage(CharacterManager character) {
            
        }
    }
}
