using Characters;
using Characters.Player;
using UnityEngine;

namespace Effects.Instant {
    public abstract class InstantCharacterEffect : ScriptableObject {
        [Header("Effect ID")]
        public int instantEffectID;

        public virtual void ProcessEffect(CharacterManager player) {
            if(player.isDead) return;
        }
    }
}
