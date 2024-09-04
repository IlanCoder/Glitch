using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Effects.Instant;
using UnityEngine;

namespace WorldManager {
    [DisallowMultipleComponent]
    public class WorldEffectsManager : MonoBehaviour {
        public static WorldEffectsManager Instance;
        
        [Header("Main Effects")]
        [SerializeField] AttackDamageEffect damageEffect;
        
        [Header("Effects")]
        [SerializeField] List<InstantCharacterEffect> instantEffects;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
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

        public AttackDamageEffect GetDamageEffectCopy(Transform parent) {
            return Instantiate(damageEffect, parent);
        }
    }
}
