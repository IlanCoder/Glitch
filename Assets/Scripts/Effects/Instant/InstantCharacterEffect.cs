using Characters;
using Characters.Player;
using UnityEngine;

namespace Effects.Instant {
    public abstract class InstantCharacterEffect : ScriptableObject {
        [Header("Effect ID")]
        public int instantEffectID;

        public abstract void ProcessEffect(CharacterManager player);
    }
}
