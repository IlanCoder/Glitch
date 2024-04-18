using System;
using System.Collections.Generic;
using Characters;
using Characters.Player;
using DataContainers;
using Effects.Instant;
using UnityEngine;
using WorldManager;

namespace Colliders {
    [RequireComponent(typeof(Collider))]
    public class DamageCollider : MonoBehaviour {
        protected InstantHealthDamageEffect DamageEffect;
        protected Collider DmgCollider;

        protected Vector3 ContactPoint;
        protected List<CharacterManager> CharactersHit = new List<CharacterManager>();

        protected virtual void Awake() {
            DmgCollider = GetComponent<Collider>();
        }

        protected virtual void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent(out CharacterManager target)) return;
            HitTarget(other, target);
        }

        protected virtual void HitTarget(Collider other, CharacterManager target) {
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

        public void SetDamage(DamageValues damage) {
            if (DamageEffect == null) DamageEffect = WorldEffectsManager.Instance.GetDamageEffectCopy(transform);
            DamageEffect.SetEffectDamage(damage);
        }
    }
}
