using System;
using System.Collections.Generic;
using Characters;
using Characters.Player;
using Effects.Instant;
using Structs;
using UnityEngine;
using WorldManager;

namespace Colliders {
    [RequireComponent(typeof(Collider))]
    public class DamageCollider : MonoBehaviour {
        protected InstantHealthDamageEffect DamageEffect;
        protected Collider DmgCollider;

        protected Vector3 ContactPoint;
        protected List<CharacterManager> CharactersHit = new List<CharacterManager>();

        void Awake() {
            DmgCollider = GetComponent<Collider>();
        }

        protected void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent(out CharacterManager target)) return;
            if (CharactersHit.Contains(target)) return;
            CharactersHit.Add(target);
            ContactPoint = other.ClosestPointOnBounds(transform.position);
            DamageTarget(target);
        }

        protected virtual void DamageTarget(CharacterManager target) {
            DamageEffect.contactPoint = ContactPoint;
            target.EffectsManager.ProcessInstantEffect(DamageEffect);
        }

        public virtual void EnableDamageCollider() {
            DmgCollider.enabled = true;
        }
        
        public virtual void DisableDamageCollider() {
            CharactersHit.Clear();
            DmgCollider.enabled = false;
        }

        public void SetDamage(DamageTypes damage) {
            if (DamageEffect == null) DamageEffect = WorldEffectsManager.Instance.GetDamageEffectCopy(transform);
            DamageEffect.InitializeEffect(damage);
        }
    }
}
