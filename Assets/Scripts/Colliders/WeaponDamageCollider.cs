using Characters;
using UnityEngine;
using WorldManager;

namespace Colliders{
	public class WeaponDamageCollider : DamageCollider {
		protected CharacterManager Wielder;
		protected override void Awake() {
			base.Awake();
			DmgCollider.enabled = false;
		}

		public virtual void SetWielder(CharacterManager newWielder) {
			Wielder = newWielder;
			if (DamageEffect == null) DamageEffect = WorldEffectsManager.Instance.GetDamageEffectCopy(transform);
			DamageEffect.characterCausingDamage = newWielder;
		}

		protected override void OnTriggerEnter(Collider other) {
			CharacterManager target = other.GetComponentInParent<CharacterManager>();
			if (!target) return;
			if (target == Wielder) return;
			if (target.CombatController.Team == Wielder.CombatController.Team) return;
			DamageEffect.hitAngle = Vector3.SignedAngle(Wielder.transform.forward, other.transform.forward, 
				Vector3.up);
			HitTarget(other, target);
		}
	}
}
