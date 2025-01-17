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
        protected AttackDamageEffect DamageEffect;
        protected Collider DmgCollider;
        
        protected List<CharacterManager> CharactersHit = new List<CharacterManager>();

        protected virtual void Awake() {
            DmgCollider = GetComponent<Collider>();
        }

        protected virtual void OnTriggerEnter(Collider other) {
            CharacterManager target = other.GetComponentInParent<CharacterManager>();
            if (!target) return;
            HitTarget(other, target);
        }

        protected virtual void HitTarget(Collider other, CharacterManager target) {
            if (CharactersHit.Contains(target)) return;
            CharactersHit.Add(target);
            DamageEffect.deflected = other.CompareTag("Deflect");
            DamageEffect.contactPoint = other.ClosestPoint(transform.position);
            DamageTarget(target);
        }

        protected virtual void DamageTarget(CharacterManager target) {
            target.EffectsController.ProcessInstantEffect(DamageEffect);
        }

        public virtual void EnableDamageCollider() {
            DmgCollider.enabled = true;
        }

        public virtual void DisableDamageCollider() {
            CharactersHit.Clear();
            DmgCollider.enabled = false;
        }

        public virtual void SetDamage(DamageStats damage) {
            DamageEffect ??= WorldEffectsManager.Instance.GetDamageEffectCopy(transform);
            DamageEffect.SetEffectDamage(damage);
        }

        public virtual void SetEnergyGain(float energyGain) {
            DamageEffect ??= WorldEffectsManager.Instance.GetDamageEffectCopy(transform);
            DamageEffect.SetEffectEnergyGain(energyGain);
        }
    }
}
