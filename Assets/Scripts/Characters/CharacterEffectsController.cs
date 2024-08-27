using System;
using Effects.Instant;
using UnityEngine;
using WorldManager;

namespace Characters {
    public class CharacterEffectsController : MonoBehaviour {
        protected CharacterManager Manager;

        protected virtual void Awake() {
            Manager = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect) {
            effect.ProcessEffect(Manager);
        }
    }
}
