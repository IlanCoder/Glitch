using System;
using System.Collections.Generic;
using Characters;
using Characters.Player;
using Effects.Instant;
using Structs;
using UnityEngine;
using WorldManager;

namespace Colliders {
    public class DamageCollider : MonoBehaviour {
        protected InstantHealthDamageEffect DamageEffect;
        
        [Header("Damage")]
        [SerializeField] protected float slashDmg;
        [SerializeField] protected float strikeDmg;
        [SerializeField] protected float thrustDmg;
        [SerializeField] protected float photonDmg;
        [SerializeField] protected float shockDmg;
        [SerializeField] protected float plasmaDmg;
        DamageTypes _damage;

        protected Vector3 ContactPoint;
        protected List<CharacterManager> CharactersHit = new List<CharacterManager>();

        void Awake() {
            _damage.SetDamage(slashDmg, strikeDmg, thrustDmg, photonDmg, shockDmg, plasmaDmg);
        }

        void Start() {
            DamageEffect = WorldEffectsManager.Instance.GetDamageEffectCopy(transform);
            DamageEffect.InitializeEffect(_damage);
        }

        protected void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent(out PlayerManager target)) return;
            if (CharactersHit.Contains(target)) return;
            CharactersHit.Add(target);
            ContactPoint = other.ClosestPointOnBounds(transform.position);
            DamageTarget(target);
        }

        protected virtual void DamageTarget(PlayerManager target) {
            DamageEffect.contactPoint = ContactPoint;
            target.effectsManager.ProcessInstantEffect(DamageEffect);
        }
    }
}
