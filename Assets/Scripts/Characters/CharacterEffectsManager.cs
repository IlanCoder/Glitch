using System;
using Effects.Instant;
using UnityEngine;

namespace Characters {
    public class CharacterEffectsManager<T> : MonoBehaviour where T: CharacterManager {
        protected T manager;
        
        protected virtual void Awake() {
            manager = GetComponent<T>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect) {
        }
    }
}
