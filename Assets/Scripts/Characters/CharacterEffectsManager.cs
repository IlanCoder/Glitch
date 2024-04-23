using System;
using Effects.Instant;
using UnityEngine;
using WorldManager;

namespace Characters {
    public class CharacterEffectsManager : MonoBehaviour {
        protected CharacterManager manager;

        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect) {
            effect.ProcessEffect(manager);
        }
    }
}
