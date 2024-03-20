using Characters;
using UnityEngine;

namespace Effects.Instant {
    public class InstantCharacterEffect : ScriptableObject {
        [Header("Effect ID")]
        public readonly int instantEffectID;

        public virtual void ProcessEffect(CharacterManager character) {
            
        }
    }
}
