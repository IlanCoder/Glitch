using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Effects.Instant;
using UnityEngine;

namespace WorldManager {
    public class WorldEffectsManager : MonoBehaviour {
        public static WorldEffectsManager Instance;
        
        [Header("Main Effects")]
        [SerializeField] InstantHealthDamageEffect damageEffect;
        
        [Header("Effects")]
        [SerializeField] List<InstantCharacterEffect> instantEffects;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
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

        public InstantHealthDamageEffect GetDamageEffectCopy(Transform parent) {
            return Instantiate(damageEffect, parent);
        }
    }
}
