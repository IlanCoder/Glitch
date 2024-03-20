using System;
using System.Collections.Generic;
using Effects.Instant;
using UnityEngine;

namespace WorldManager {
    public class WorldEffectsManager : MonoBehaviour {
        [Header("Effects")]
        [SerializeField] List<InstantCharacterEffect> instantEffects;

        void Awake() {
            GenerateEffectsIDs();
        }

        void GenerateEffectsIDs() {
            GenerateInstantEffectsIDs();
        }

        void GenerateInstantEffectsIDs() {
            for (int i = 0; i < instantEffects.Count; i++) {
                instantEffects[i].instantEffectID = i;
            }
        }
    }
}
