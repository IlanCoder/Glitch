using Characters;
using UnityEngine;

namespace Colliders{
	public class WeaponDamageCollider : DamageCollider {
		protected CharacterManager Wielder;
		protected override void Awake() {
			base.Awake();
			DmgCollider.enabled = false;
		}

		public virtual void SetWielder(CharacterManager newWielder) {
			Wielder = newWielder;
		}

		protected override void OnTriggerEnter(Collider other) {
			if (!other.TryGetComponent(out CharacterManager target)) return;
			if (target == Wielder) return;
			HitTarget(other, target);
		}
	}
}
