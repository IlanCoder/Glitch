using System;
using UnityEngine;
using Effects.Instant;

namespace Characters.Player {
    public class PlayerEffectsManager : CharacterEffectsManager {
        public override void ProcessInstantEffect(InstantCharacterEffect effect) {
            effect.ProcessEffect(manager);
        }
    }
}
