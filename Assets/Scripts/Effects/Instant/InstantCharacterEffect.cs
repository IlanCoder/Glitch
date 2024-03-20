using Characters;
using Characters.Player;
using UnityEngine;

namespace Effects.Instant {
    public class InstantCharacterEffect : ScriptableObject {
        [Header("Effect ID")]
        public int instantEffectID;

        public virtual void ProcessEffect(PlayerManager player) {
            
        }
    }
}
