using System;
using Effects.Instant;
using Enums;
using UnityEngine;
using WorldManager;

namespace Characters {
    public class CharacterEffectsManager : MonoBehaviour {
        protected CharacterManager manager;

        [SerializeField] DamageVFxType damageVFxType;
        
        protected virtual void Awake() {
            manager = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect) {
            effect.ProcessEffect(manager);
        }

        public virtual void PlayDamageVFx(Vector3 contactPoint) {
            GameObject vFx = WorldVFxsManager.Instance.GetDamageVFxInstance(damageVFxType);
            vFx.transform.position = contactPoint;
            if(!vFx.TryGetComponent(out ParticleSystem particle)) return;
            particle.Play();
        }
    }
}
